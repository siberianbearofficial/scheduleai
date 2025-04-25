import {inject, Injectable} from '@angular/core';
import {patchState, signalState} from '@ngrx/signals';
import {GroupEntity} from '../entities/group-entity';
import {toObservable} from '@angular/core/rxjs-interop';
import {EMPTY, map, Observable, switchMap, tap} from 'rxjs';
import {BffClient, Group} from '../bff-client/bff-client';
import {UniversitiesService} from './universities.service';
import {LoadingStatus} from '../entities/LoadingStatus';

interface GroupStore {
  readonly groups: GroupEntity[];
  readonly selectedGroup: GroupEntity | null;
  loadingGroupsStatus: LoadingStatus;
  loadingSelectedStatus: LoadingStatus;
}

const selectedGroupKey = "scheduleai-selectedGroup";

@Injectable({
  providedIn: 'root'
})
export class GroupsService {

  private readonly bffClient: BffClient = inject(BffClient);
  private readonly universityService: UniversitiesService = inject(UniversitiesService);

  private readonly groups$$ = signalState<GroupStore>({
    groups: [],
    selectedGroup: null,
    loadingGroupsStatus: LoadingStatus.NotStarted,
    loadingSelectedStatus: LoadingStatus.NotStarted,
  })

  readonly groups$ = toObservable(this.groups$$).pipe(
    map(store => store.groups)
  );

  readonly selectedGroup$ = toObservable(this.groups$$).pipe(
    map(store => store.selectedGroup),
  )

  private loadGroups(universityId: string): Observable<undefined> {
    patchState(this.groups$$, {loadingGroupsStatus: LoadingStatus.InProgress});
    return this.bffClient.groups(universityId, "").pipe(
      tap(resp => console.log(resp.detail)),
      map(resp => resp.data?.map(groupToEntity)),
      tap(groups => patchState(this.groups$$, {groups})),
      tap(() => patchState(this.groups$$, {loadingGroupsStatus: LoadingStatus.Completed})),
      tap(groups => {
        const selectedGroupId = localStorage.getItem(selectedGroupKey);
        groups = groups?.filter(u => u.id === selectedGroupId);
        if (groups == undefined || groups.length != 1) {
          patchState(this.groups$$, {selectedGroup: null, loadingSelectedStatus: LoadingStatus.Failed})
        } else {
          patchState(this.groups$$, {
            selectedGroup: groups[0],
            loadingSelectedStatus: LoadingStatus.Completed
          });
        }
      }),
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

  readonly saveSelectedGroup$: Observable<undefined> = toObservable(this.groups$$).pipe(
    tap(store => {
      if (store.loadingSelectedStatus != LoadingStatus.NotStarted) {
        if (store.selectedGroup === null) {
          localStorage.removeItem(selectedGroupKey);
        } else {
          localStorage.setItem(selectedGroupKey, store.selectedGroup.id);
        }
      }
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
