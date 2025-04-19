import {DestroyRef, inject, Injectable} from '@angular/core';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {TUI_DEFAULT_MATCHER} from '@taiga-ui/cdk';
import {BehaviorSubject, map, Observable} from 'rxjs';
import {
  delay,
  distinctUntilChanged,
  of,
  shareReplay,
  startWith,
  Subject,
  switchMap,
} from 'rxjs';

import {databaseMockData} from './database.service';
import type {Group} from './group.service';

export interface Teacher {
  readonly id: string;
  readonly fullName: string;
  readonly avatarUrl: string;
  readonly department: string;
}


@Injectable({
    providedIn: 'root'
})
export class TeacherService {
  private readonly destroyRef = inject(DestroyRef);
  private readonly request$ = new Subject<string>();

  // Imitating server request with switchMap + delay pair
  private readonly response$ = this.request$.pipe(
    distinctUntilChanged(),
    switchMap((query) =>
      of(databaseMockData.filter((user) => TUI_DEFAULT_MATCHER(user, query))).pipe(
        delay(Math.random() * 1000 + 500),
        startWith(null),
      ),
    ),
    takeUntilDestroyed(this.destroyRef),
    shareReplay({bufferSize: 1, refCount: true}),
  );

  public request(query: string | null): Observable<readonly Group[] | null> {
    this.request$.next(query || '');

    return this.response$;
  }

  readonly teachers$ = new BehaviorSubject<Teacher[]>([
    {
      id: 'test',
      fullName: 'Ryaxan',
      avatarUrl: 'https://i.imgur.com/tKuNxnA.jpeg',
      department: 'IU7',
    },
    {
      id: 'test1',
      fullName: 'Ryaxan',
      avatarUrl: 'https://i.imgur.com/tKuNxnA.jpeg',
      department: 'IU7',
    },
    {
      id: 'test2',
      fullName: 'Ryaxan',
      avatarUrl: 'https://i.imgur.com/tKuNxnA.jpeg',
      department: 'IU7',
    },
    {
      id: 'test3',
      fullName: 'Ryaxan',
      avatarUrl: 'https://i.imgur.com/tKuNxnA.jpeg',
      department: 'IU7',
    },
    {
      id: 'test4',
      fullName: 'Ryaxan',
      avatarUrl: 'https://i.imgur.com/tKuNxnA.jpeg',
      department: 'IU7',
    },
    {
      id: 'test5',
      fullName: 'Ryaxan',
      avatarUrl: 'https://i.imgur.com/tKuNxnA.jpeg',
      department: 'IU7',
    },
    {
      id: 'test6',
      fullName: 'Ryaxan',
      avatarUrl: 'https://i.imgur.com/tKuNxnA.jpeg',
      department: 'IU7',
    },{
      id: 'test7',
      fullName: 'Ryaxan',
      avatarUrl: 'https://i.imgur.com/tKuNxnA.jpeg',
      department: 'IU7',
    },{
      id: 'test8',
      fullName: 'Ryaxan',
      avatarUrl: 'https://i.imgur.com/tKuNxnA.jpeg',
      department: 'IU7',
    },
  ]);

  teacherById(teacherId: string): Observable<Teacher | undefined> {
    return this.teachers$.pipe(
      map(teachers => teachers.find(teachers => teachers.id == teacherId)),
    );
  }
}

