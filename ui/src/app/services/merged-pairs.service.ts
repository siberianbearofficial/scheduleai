import {inject, Injectable} from '@angular/core';
import {patchState, signalState} from '@ngrx/signals';
import moment, {duration, Moment} from 'moment';
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

interface PairsRangeStore {
  readonly startTime: Moment;
  readonly endTime: Moment;
}


interface MergedPairsStore {
  readonly mergedPairs: PairEntity[];
}

@Injectable({
  providedIn: 'root'
})
export class MergedPairsService {
  private readonly bffClient: BffClient = inject(BffClient);
  private readonly universityService: UniversitiesService = inject(UniversitiesService);
  private readonly groupsService: GroupsService = inject(GroupsService);
  private readonly teacherService: TeacherService = inject(TeacherService);


  private readonly pairsRange$$ = signalState<PairsRangeStore>({
    startTime: moment(),
    endTime: moment().add(7, 'days'),
  })

  private readonly mergedPairs$$ = signalState<MergedPairsStore>({
    mergedPairs: [],
  })

  readonly mergedPairs$: Observable<PairEntity[]> = toObservable(this.mergedPairs$$.mergedPairs).pipe(
    tap(console.log),
  );

  setDateRange(startDate: Moment, endDate: Moment): void {
    patchState(this.pairsRange$$, {startTime: startDate, endTime: endDate});
  }

  private loadMergedPairs(universityId: string, groupId: string, teacherId: string, startTime: Moment, endTime: Moment): Observable<undefined> {
    return this.bffClient.mergedPairs(MergedPairsRequestSchema.fromJS({
      universityId: universityId,
      groupId: groupId,
      teacherId: teacherId,
      startTime: startTime.toDate(),
      endTime: endTime.toDate(),
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
    this.teacherService.selectedTeacher$,
    toObservable(this.pairsRange$$),
  ]).pipe(
    switchMap(([university, group, teacher, range]) => {
      console.log(`University: ${university}, Group: ${group}, Teacher: ${teacher}`)
      if (university !== null && group !== null && teacher !== null)
        return this.loadMergedPairs(university.id, group.id, teacher.id, range.startTime, range.endTime)
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

