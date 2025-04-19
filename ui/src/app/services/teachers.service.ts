import {Injectable} from '@angular/core';
import {toObservable} from '@angular/core/rxjs-interop';
import {BehaviorSubject, map, Observable} from 'rxjs';
import {signalState} from '@ngrx/signals';
import {TeacherEntity} from '../entities/teacher-entity';


@Injectable({
  providedIn: 'root'
})
export class TeacherService {
  private readonly teachers$$ = signalState<TeacherEntity[]>([
    {
      id: 'test',
      fullName: 'Ryaxan',
      departments: [],
    },
  ]);

  readonly teachers$ = toObservable(this.teachers$$);

  teacherById(teacherId: string): Observable<TeacherEntity | undefined> {
    return this.teachers$.pipe(
      map(teachers => teachers.find(teachers => teachers.id == teacherId)),
    );
  }

  private readonly selectedTeacher$$ = new BehaviorSubject<TeacherEntity | null>(null);

  readonly selectedTeacher$ = this.selectedTeacher$$.pipe();
}

