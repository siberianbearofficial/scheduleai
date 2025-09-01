import {inject, Injectable} from '@angular/core';
import {patchState, signalState} from '@ngrx/signals';
import {GroupEntity} from '../entities/group-entity';
import {toObservable} from '@angular/core/rxjs-interop';
import {EMPTY, map, Observable, switchMap, tap} from 'rxjs';
import {BffClient, Group} from '../bff-client/bff-client';
import {UniversitiesService} from './universities.service';
import {LoadingStatus} from '../entities/LoadingStatus';
import {StorageService} from './storage.service';

interface GroupStore {
  readonly groups: GroupEntity[];
  readonly selectedGroup: GroupEntity | null;
  readonly loadingGroupsStatus: LoadingStatus;
  readonly loadingSelectedStatus: LoadingStatus;
}

@Injectable({
  providedIn: 'root'
})
export class GroupsService {
  private readonly bffClient: BffClient = inject(BffClient);
  private readonly universityService: UniversitiesService = inject(UniversitiesService);
  private readonly storageService: StorageService = inject(StorageService)

  private readonly groups$$ = signalState<GroupStore>({
    groups: [],
    selectedGroup: null,
    loadingGroupsStatus: LoadingStatus.NotStarted,
    loadingSelectedStatus: LoadingStatus.NotStarted,
  })

  readonly groupsInfo$ = toObservable(this.groups$$);

  readonly groups$ = toObservable(this.groups$$.groups);

  readonly selectedGroup$ = toObservable(this.groups$$.selectedGroup);

  private loadGroups(universityId: string): Observable<undefined> {
    patchState(this.groups$$, {loadingGroupsStatus: LoadingStatus.InProgress});
    return this.bffClient.groups(universityId, "").pipe(
      tap(resp => console.log(resp.detail)),
      map(resp => resp.data?.map(groupToEntity)),
      tap(groups => patchState(this.groups$$, {
        groups,
        loadingGroupsStatus: LoadingStatus.Completed,
        selectedGroup: this.storageService.getGroup(groups ?? []),
        loadingSelectedStatus: LoadingStatus.Completed,
      })),
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
        this.storageService.setGroup(store.selectedGroup)
      }
    }),
    switchMap(() => EMPTY),
  )

  selectGroup(group: GroupEntity): void {
    patchState(this.groups$$, {
      selectedGroup: group
    })
  }

  groupById(groupId: string): Observable<GroupEntity | undefined> {
    return this.groups$.pipe(
      map(groups => groups.find(g => g.id == groupId)),
    );
  }

}

const groupToEntity = (group: Group): GroupEntity => ({
  id: group.id ?? "",
  name: group.name ?? "<Неизвестная группа>",
});
