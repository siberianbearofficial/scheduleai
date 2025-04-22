import {inject, Injectable} from '@angular/core';
import {patchState, signalState} from '@ngrx/signals';
import {MergedPairEntity, MergedPairStatus} from '../entities/merged-pairs-entity';
import moment from 'moment';
import {toObservable} from '@angular/core/rxjs-interop';
import {combineLatest, EMPTY, map, Observable, switchMap, tap} from 'rxjs';
import {BffClient, MergedPair, MergedPairsRequestSchema, Pair} from '../bff-client/bff-client';
import {UniversitiesService} from './universities.service';
import {GroupsService} from './groups.service';
import {TeacherService} from './teachers.service';
import {PairEntity} from '../entities/pair-entity';

interface MergedPairsStore {
  mergedPairs: MergedPairEntity[];
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

  readonly mergedPairs$: Observable<MergedPairEntity[]> = toObservable(this.mergedPairs$$.mergedPairs).pipe(
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
      map(resp => resp.data?.map(mergedPairToEntity)),
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

const parseTimeSpan = (timeString: string): number => {
  const [hours, minutes, seconds] = timeString.split(':').map(Number);
  return (hours * 3600 + minutes * 60 + seconds);
}

const parseMergedPairStatus = (status: number): MergedPairStatus => {
  switch (status) {
    case 0:
      return MergedPairStatus.beforePairs;
    case 1:
      return MergedPairStatus.afterPairs;
    case 2:
      return MergedPairStatus.inGap;
    case 3:
      return MergedPairStatus.collision;
    default:
      return MergedPairStatus.beforePairs;
  }
}

const mergedPairToEntity = (pair: MergedPair): MergedPairEntity => ({
  startTime: moment(pair.startTime),
  endTime: moment(pair.endTime),
  actType: pair.actType ?? "",
  discipline: pair.discipline ?? "",
  convenience: pair.convenience,
  rooms: pair.rooms ?? [],
  status: parseMergedPairStatus(pair.status ?? 0),
  collisions: pair.collisions?.map(pairToEntity) ?? [],
  waitTimeSec: parseTimeSpan(pair.waitTime ?? "00:00:00")
});

const pairToEntity = (pair: Pair): PairEntity => ({
  groups: pair.groups ?? [],
  teachers: pair.teachers ?? [],
  startTime: moment(pair.startTime),
  endTime: moment(pair.endTime),
  actType: pair.actType ?? "",
  discipline: pair.discipline ?? "",
  rooms: pair.rooms ?? [],
});

