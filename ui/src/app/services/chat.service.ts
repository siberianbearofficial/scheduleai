import {inject, Injectable} from '@angular/core';
import {AiHelperRequestModel, BffClient} from '../bff-client/bff-client';
import {patchState, signalState} from '@ngrx/signals';
import {MessageEntity, MessageRole} from '../entities/message-entity';
import {toObservable} from '@angular/core/rxjs-interop';
import {combineLatest, EMPTY, Observable, switchMap, tap} from 'rxjs';
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

  private addMessage(message: MessageEntity) {
    patchState(this.messages$$, store => {
      store.messages.push(message);
      return store;
    })
  }

  sendMessage(message: string): Observable<undefined> {
    this.addMessage({
      role: MessageRole.User,
      text: message,
      timestamp: moment(),
      pairs: [],
    });
    return combineLatest([this.universityService.selectedUniversity$, this.groupsService.selectedGroup$]).pipe(
      switchMap(([university, group]) => this.bffClient.aiHelper(AiHelperRequestModel.fromJS({
        universityId: university?.id,
        groupId: group?.id,
        prompt: message,
      }))),
      tap(resp => console.log(resp.detail)),
      tap(resp => this.addMessage({
        role: MessageRole.Assistant,
        text: resp.data.text ?? "",
        timestamp: moment(),
        pairs: resp.data.pairs?.map(pairToEntity) ?? [],
      })),
      switchMap(() => EMPTY),
    )
  }
}
