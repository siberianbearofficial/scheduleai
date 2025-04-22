import {inject, Injectable} from '@angular/core';
import {toObservable} from '@angular/core/rxjs-interop';
import {EMPTY, map, Observable, switchMap, tap} from 'rxjs';
import {patchState, signalState} from '@ngrx/signals';
import {TeacherEntity} from '../entities/teacher-entity';
import {BffClient, Teacher} from '../bff-client/bff-client';
import {UniversitiesService} from './universities.service';

interface TeacherStore {
  teachers: TeacherEntity[];
  selectedTeacher: TeacherEntity | null;
}


@Injectable({
  providedIn: 'root'
})
export class TeacherService {
  private readonly bffClient: BffClient = inject(BffClient);
  private readonly universityService: UniversitiesService = inject(UniversitiesService);

  private readonly teachers$$ = signalState<TeacherStore>({
    teachers: [],
    selectedTeacher: null
  });

  readonly teachers$ = toObservable(this.teachers$$).pipe(
    map(store => store.teachers)
  );

  teacherById(teacherId: string): Observable<TeacherEntity | undefined> {
    return this.teachers$.pipe(
      map(teachers => teachers.find(teachers => teachers.id == teacherId)),
    );
  }

  readonly selectedTeacher$ = toObservable(this.teachers$$).pipe(
    map(store => store.selectedTeacher)
  );

  private loadTeachers(universityId: string): Observable<undefined> {
    return this.bffClient.teachers(universityId, "").pipe(
      tap(resp => console.log(resp.detail)),
      map(resp => resp.data?.map(teacherToEntity2)),
      tap(teachers => patchState(this.teachers$$, {teachers})),
      switchMap(() => EMPTY),
    )
  }

  readonly loadTeachersOnUniversityChange$: Observable<undefined> = this.universityService.selectedUniversity$.pipe(
    switchMap(selected => {
      if (selected !== null)
        return this.loadTeachers(selected.id)
      return EMPTY;
    }),
    switchMap(() => EMPTY),
  )

  selectTeacher(teacher: TeacherEntity | null): void {
    patchState(this.teachers$$, {
      selectedTeacher: teacher
    })
  }
}

const teacherToEntity2 = (teacher: Teacher): TeacherEntity => ({
  id: teacher.id ?? "",
  fullName: teacher.fullName ?? "<Неизвестный преподаватель>",
  departments: [],
  avatarUrl: "https://i.imgur.com/tKuNxnA.jpeg"
});
