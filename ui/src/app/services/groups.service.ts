import {inject, Injectable} from '@angular/core';
import {patchState, signalState} from '@ngrx/signals';
import {GroupEntity} from '../entities/group-entity';
import {toObservable} from '@angular/core/rxjs-interop';
import { EMPTY, map, Observable, switchMap, tap} from 'rxjs';
import {BffClient, Group} from '../bff-client/bff-client';
import {UniversitiesService} from './universities.service';

interface GroupStore {
  readonly groups: GroupEntity[];
  readonly selectedGroup: GroupEntity | null;
}

@Injectable({
  providedIn: 'root'
})
export class GroupsService {

  private readonly bffClient: BffClient = inject(BffClient);
  private readonly universityService: UniversitiesService = inject(UniversitiesService);

  private readonly groups$$ = signalState<GroupStore>({
    groups: [],
    selectedGroup: null,
  })

  readonly groups$ = toObservable(this.groups$$).pipe(
    map(store => store.groups)
  );

  readonly selectedGroup$ = toObservable(this.groups$$).pipe(
    map(store => store.selectedGroup),
  )

  private loadGroups(universityId: string): Observable<undefined> {
    console.log(`Loading ${universityId}`)
    return this.bffClient.groups(universityId).pipe(
      tap(resp => console.log(resp.detail)),
      map(resp => resp.data?.map(groupToEntity)),
      tap(groups => patchState(this.groups$$, {groups})),
      switchMap(() => EMPTY),
    )
  }

  readonly loadGroupsOnUniversityChange$: Observable<undefined> = this.universityService.selectedUniversity$.pipe(
    switchMap(selected => {
      console.log(selected);
      if (selected !== null)
        return this.loadGroups(selected.id)
      return EMPTY;
    }),
    switchMap(() => EMPTY),
  )

  selectGroup(group: GroupEntity): void {
    patchState(this.groups$$, {
      selectedGroup: group
    })
  }

}

const groupToEntity = (group: Group): GroupEntity => ({
  id: group.id ?? "",
  name: group.name ?? "<Неизвестная группа>",
});
