import {Injectable} from '@angular/core';
import {signalState} from '@ngrx/signals';
import {GroupEntity} from '../entities/group-entity';
import {toObservable} from '@angular/core/rxjs-interop';
import {BehaviorSubject, filter, map, Observable} from 'rxjs';

interface GroupStore {
  readonly groups: GroupEntity[];
  readonly selectedGroup: GroupEntity | null;
}

@Injectable({
  providedIn: 'root'
})
export class GroupsService {

  private readonly groups$$ = signalState<GroupStore>({
    groups: [
      {
        id: "1",
        name: "ИУ7-11Б",
      },
      {
        id: "2",
        name: "ИУ7-12Б",
      },
      {
        id: "3",
        name: "ИУ7-31Б",
      },
      {
        id: "4",
        name: "ИУ7-33Б",
      },
      {
        id: "5",
        name: "ИУ7-52Б",
      },
      {
        id: "6",
        name: "ИУ7-56Б",
      },
    ],
    selectedGroup: null,
  })

  readonly groups$ = toObservable(this.groups$$).pipe(
    map(store => store.groups)
  );

  readonly selectedGroup$ = toObservable(this.groups$$).pipe(
    map(store => store.selectedGroup),
  )

  findGroups(query: string | null): Observable<readonly GroupEntity[] | null> {
    return this.groups$.pipe(
      map(groups => groups.filter(group => group.name == query)),
    )
  }
}
