import {inject, Injectable} from '@angular/core';
import {
  AiHelperRequestModel,
  AiHelperTask,
  AiHelperTaskStatus,
  AiHelperToolCall,
  BffClient
} from '../bff-client/bff-client';
import {patchState, signalState} from '@ngrx/signals';
import {MessageEntity, MessageRole, ToolCallEntity} from '../entities/message-entity';
import {toObservable} from '@angular/core/rxjs-interop';
import {combineLatest, concatMap, EMPTY, interval, Observable, of, switchMap, take, tap} from 'rxjs';
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
        messages: [...store.messages, message]
      };
    });
    return message;
  }

  private replaceMessage(oldMessage: MessageEntity, newMessage: MessageEntity): MessageEntity {
    patchState(this.messages$$, store => {
      const messages = store.messages.slice();
      const index = messages.findIndex(m => m == oldMessage);
      if (index === undefined)
        messages.push(newMessage);
      else
        messages[index] = newMessage;
      return {messages};
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
      concatMap(resp => this.pollTask(resp.data))
    )
  }

  private pollTask(task: AiHelperTask): Observable<boolean> {
    let message: MessageEntity = this.addMessage(messageFromTask(task, true));
    return interval(1000).pipe(
      switchMap(() => this.bffClient.aiHelperGET(task.id).pipe(
        tap(resp => console.log(resp.detail)),
        switchMap(resp => {
          if (resp.data.status == AiHelperTaskStatus._2) {  // COMPLETED
            message = this.replaceMessage(message, messageFromTask(resp.data, false));
            return of(true);
          }
          if (resp.data.status == AiHelperTaskStatus._3) {  // FAILED
            message = this.replaceMessage(message, messageFromTask(resp.data, false));
            return of(false)
          }
          if (resp.data.toolCalls?.length !== message.toolCalls.length) {
            message = this.replaceMessage(message, messageFromTask(resp.data, true))
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

const messageFromTask = (task: AiHelperTask, inProgress: boolean): MessageEntity => ({
  html: task.response?.text ?? null,
  role: MessageRole.Assistant,
  timestamp: moment(),
  pairs: task.response?.pairs?.map(pairToEntity) ?? [],
  toolCalls: task.toolCalls?.map(toolCallToEntity) ?? [],
  inProgress: inProgress,
});
