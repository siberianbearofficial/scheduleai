import {Injectable} from '@angular/core';
import {toObservable} from '@angular/core/rxjs-interop';
import {BehaviorSubject, map, Observable, tap} from 'rxjs';
import {signalState} from '@ngrx/signals';
import {TeacherEntity} from '../entities/teacher-entity';

interface TeacherStore {
  teachers: TeacherEntity[];
  selectedTeacher: TeacherEntity | null;
}


@Injectable({
  providedIn: 'root'
})
export class TeacherService {
  private readonly teachers$$ = signalState<TeacherStore>({
    teachers: [
      {
        id: 'test',
        fullName: 'Ryaxan',
        departments: [],
        avatarUrl: "https://i.imgur.com/tKuNxnA.jpeg"
      },
    ],
    selectedTeacher: null
  });

  readonly teachers$ = toObservable(this.teachers$$).pipe(
    map(store => store.teachers)
  );

  teacherById(teacherId: string): Observable<TeacherEntity | undefined> {
    console.log("TeacherById")
    return this.teachers$.pipe(
      map(teachers => teachers.find(teachers => teachers.id == teacherId)),
      tap(console.log),
    );
  }

  readonly selectedTeacher$ = toObservable(this.teachers$$).pipe(
    map(store => store.selectedTeacher)
  );
}

