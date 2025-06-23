import {inject, Injectable} from '@angular/core';
import {AiHelperRequestModel, AiHelperToolCall, BffClient} from '../bff-client/bff-client';
import {patchState, signalState} from '@ngrx/signals';
import {MessageEntity, MessageRole, ToolCallEntity} from '../entities/message-entity';
import {toObservable} from '@angular/core/rxjs-interop';
import {combineLatest, EMPTY, interval, Observable, of, switchMap, take, tap} from 'rxjs';
import {UniversitiesService} from './universities.service';
import {GroupsService} from './groups.service';
import moment from 'moment';
import {pairToEntity} from './merged-pairs.service';

interface MessagesStore {
  messages: MessageEntity[];
}

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private readonly bffClient: BffClient = inject(BffClient);
  private readonly universityService: UniversitiesService = inject(UniversitiesService);
  private readonly groupsService: GroupsService = inject(GroupsService);

  private readonly messages$$ = signalState<MessagesStore>({
    messages: [],
  });

  readonly messages$ = toObservable(this.messages$$.messages);

  private addMessage(message: MessageEntity): MessageEntity {
    patchState(this.messages$$, store => {
      return {
        messages: store.messages.concat([message])
      };
    });
    return message;
  }

  private replaceMessage(oldMessage: MessageEntity, newMessage: MessageEntity): MessageEntity {
    patchState(this.messages$$, store => {
      const messages = store.messages.slice();
      const index = messages.findIndex(m => m == oldMessage);
      messages[index] = newMessage;
      return {
        messages: messages
      };
    });
    return newMessage;
  }

  sendMessage(message: string): Observable<boolean> {
    this.addMessage({
      role: MessageRole.User,
      html: message,
      timestamp: moment(),
      pairs: [],
      toolCalls: [],
      inProgress: false,
    });
    return combineLatest([this.universityService.selectedUniversity$, this.groupsService.selectedGroup$]).pipe(
      switchMap(([university, group]) => this.bffClient.aiHelperPOST(AiHelperRequestModel.fromJS({
        universityId: university?.id,
        groupId: group?.id,
        prompt: message,
      }))),
      tap(resp => console.log(resp.detail)),
      switchMap(resp => this.pollTask(resp.data.id))
    )
  }

  private pollTask(taskId: string): Observable<boolean> {
    let message: MessageEntity = this.addMessage({
      html: null,
      role: MessageRole.Assistant,
      timestamp: moment(),
      pairs: [],
      toolCalls: [],
      inProgress: true,
    });
    return interval(1000).pipe(
      switchMap(() => this.bffClient.aiHelperGET(taskId).pipe(
        tap(resp => console.log(resp.detail)),
        switchMap(resp => {
          if (resp.data.status == 2) {
            message = this.replaceMessage(message, {
              html: resp.data.response?.text ?? null,
              role: MessageRole.Assistant,
              timestamp: moment(),
              pairs: resp.data.response?.pairs?.map(pairToEntity) ?? [],
              toolCalls: message.toolCalls,
              inProgress: false,
            });
            return of(true);
          }
          if (resp.data.status == 3) {
            message = this.replaceMessage(message, {
              html: null,
              role: MessageRole.Assistant,
              timestamp: moment(),
              pairs: [],
              toolCalls: message.toolCalls,
              inProgress: false,
            });
            return of(false)
          }
          if (resp.data.toolCalls?.length != message.toolCalls.length) {
            message = this.replaceMessage(message, {
              html: null,
              role: MessageRole.Assistant,
              timestamp: moment(),
              pairs: [],
              toolCalls: resp.data.toolCalls?.map(toolCallToEntity) ?? [],
              inProgress: true,
            })
          }
          return EMPTY;
        }),
        take(1),
      )),
    )
  }
}

const toolCallToEntity = (toolCall: AiHelperToolCall): ToolCallEntity => ({
  name: toolCall.toolName ?? "unknown",
  description: toolCall.toolDescription ?? "",
  parameter: toolCall.parameter ?? "",
  result: toolCall.result ?? null,
  isSuccess: toolCall.isSuccess ?? false,
  error: toolCall.errorMessage ?? null,
});
