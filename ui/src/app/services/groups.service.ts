import {Injectable} from '@angular/core';
import {signalState} from '@ngrx/signals';
import {GroupEntity} from '../entities/group-entity';
import {toObservable} from '@angular/core/rxjs-interop';
import {BehaviorSubject, filter, map, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GroupsService {

  private readonly groups$$ = signalState<GroupEntity[]>([
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
  ])

  readonly groups$ = toObservable(this.groups$$);

  private readonly selectedGroup$$ = new BehaviorSubject<GroupEntity | null>(null);

  readonly selectedGroup$ = this.selectedGroup$$.pipe();

  findGroups(query: string | null): Observable<readonly GroupEntity[] | null> {
    return this.groups$.pipe(
      map(groups => groups.filter(group => group.name == query)),
    )
  }
}
