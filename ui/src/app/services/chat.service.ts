import {inject, Injectable} from '@angular/core';
import {AiHelperRequestModel, BffClient} from '../bff-client/bff-client';
import {patchState, signalState} from '@ngrx/signals';
import {MessageEntity, MessageRole} from '../entities/message-entity';
import {toObservable} from '@angular/core/rxjs-interop';
import {combineLatest, EMPTY, Observable, switchMap, tap} from 'rxjs';
import {UniversitiesService} from './universities.service';
import {GroupsService} from './groups.service';
import moment, {duration} from 'moment';
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
    messages: [
      {
        role: MessageRole.User,
        text: "Во сколько завтра я могу встретиться с Куровым?",
        timestamp: moment(),
        pairs: [],
      },
      {
        role: MessageRole.Assistant,
        text: "Вы можете встретиться с <b>Куровым Андреем Владимировичем</b>, завтра, 29 апреля:<ul>  <li><b>с 12:05 до 13:05</b>, лекция <i>«Базы Данных»</i>, аудитория <b>102x</b></li>  <li><b>с 15:40 до 16:10</b>, семинар <i>«Компьютерная Графика»</i>, аудитория <b>304x</b></li></ul>",
        timestamp: moment(),
        pairs: [
          {
            "teachers": ["Куров Андрей Владимирович"],
            "groups": ["Группа 101", "Группа 102"],
            "startTime": moment("2025-04-27T12:05:00"),
            "endTime": moment("2025-04-27T13:05:00"),
            "rooms": ["102x"],
            "discipline": "Базы данных",
            "actType": "Лекция",
            "convenience": {"coefficient": 0.5, "status": 0, waitTime: duration(0), collisions: []}
          }, {
            "teachers": ["Куров Андрей Владимирович"],
            "groups": ["Группа 103"],
            "startTime": moment("2025-04-27T15:40:00"),
            "endTime": moment("2025-04-27T16:10:00"),
            "rooms": ["304x"],
            "discipline": "Компьютерная графика",
            "actType": "Семинар",
            "convenience": {"coefficient": 0.7, "status": 0, waitTime: duration(0), collisions: []}
          }],
      },
    ]
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
