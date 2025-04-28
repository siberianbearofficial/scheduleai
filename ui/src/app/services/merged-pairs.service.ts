import {inject, Injectable} from '@angular/core';
import {patchState, signalState} from '@ngrx/signals';
import moment, {duration} from 'moment';
import {toObservable} from '@angular/core/rxjs-interop';
import {combineLatest, EMPTY, map, Observable, switchMap, tap} from 'rxjs';
import {
  BffClient,
  MergedPairsRequestSchema,
  MergedPairStatus as BffMergedPairStatus,
  Pair
} from '../bff-client/bff-client';
import {UniversitiesService} from './universities.service';
import {GroupsService} from './groups.service';
import {TeacherService} from './teachers.service';
import {MergedPairStatus, PairEntity} from '../entities/pair-entity';

interface MergedPairsStore {
  mergedPairs: PairEntity[];
}

@Injectable({
  providedIn: 'root'
})
export class MergedPairsService {
  private readonly bffClient: BffClient = inject(BffClient);
  private readonly universityService: UniversitiesService = inject(UniversitiesService);
  private readonly groupsService: GroupsService = inject(GroupsService);
  private readonly teacherService: TeacherService = inject(TeacherService);


  private readonly mergedPairs$$ = signalState<MergedPairsStore>({
    mergedPairs: [],
  })

  readonly mergedPairs$: Observable<PairEntity[]> = toObservable(this.mergedPairs$$.mergedPairs).pipe(
    tap(console.log),
  );

  private loadMergedPairs(universityId: string, groupId: string, teacherId: string): Observable<undefined> {
    return this.bffClient.mergedPairs(MergedPairsRequestSchema.fromJS({
      universityId: universityId,
      groupId: groupId,
      teacherId: teacherId,
      startTime: moment().toDate(),
      endTime: moment().add(7, 'days').toDate(),
    })).pipe(
      tap(resp => console.log(resp.detail)),
      map(resp => resp.data?.map(pairToEntity)),
      tap(mergedPairs => patchState(this.mergedPairs$$, {
        mergedPairs: mergedPairs ?? []
      })),
      switchMap(() => EMPTY),
    )
  }

  readonly loadMergedPairsOnUniversityChange$: Observable<undefined> = combineLatest([
    this.universityService.selectedUniversity$,
    this.groupsService.selectedGroup$,
    this.teacherService.selectedTeacher$
  ]).pipe(
    switchMap(([university, group, teacher]) => {
      console.log(`University: ${university}, Group: ${group}, Teacher: ${teacher}`)
      if (university !== null && group !== null && teacher !== null)
        return this.loadMergedPairs(university.id, group.id, teacher.id)
      return EMPTY;
    }),
    switchMap(() => EMPTY),
  )
}

const mergedStatusMap: Record<BffMergedPairStatus, MergedPairStatus> = {
  [BffMergedPairStatus._0]: MergedPairStatus.BeforePairs,
  [BffMergedPairStatus._1]: MergedPairStatus.AfterPairs,
  [BffMergedPairStatus._2]: MergedPairStatus.InGap,
  [BffMergedPairStatus._3]: MergedPairStatus.Collision,
  [BffMergedPairStatus._4]: MergedPairStatus.This,
  [BffMergedPairStatus._5]: MergedPairStatus.NoPairs,
} as const;

export const pairToEntity = (pair: Pair): PairEntity => ({
  startTime: moment(pair.startTime),
  endTime: moment(pair.endTime),
  actType: pair.actType ?? "",
  discipline: pair.discipline ?? "",
  rooms: pair.rooms ?? [],
  groups: pair.groups ?? [],
  teachers: pair.teachers ?? [],
  convenience: pair.convenience === null ? null : {
    coefficient: pair.convenience?.coefficient ?? 0,
    status: mergedStatusMap[pair.convenience?.status ?? BffMergedPairStatus._3],
    collisions: pair.convenience?.collisions?.map(pairToEntity) ?? [],
    waitTime: duration(pair.convenience?.waitTime ?? "00:00:00"),
  }
});

