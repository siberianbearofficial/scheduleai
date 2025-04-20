import {inject, Injectable} from '@angular/core';
import {patchState, signalState} from '@ngrx/signals';
import {toObservable} from '@angular/core/rxjs-interop';
import {EMPTY, map, Observable, switchMap, tap} from 'rxjs';
import {UniversityEntity} from '../entities/university-entity';
import {BffClient, University} from '../bff-client/bff-client';

interface UniversityStore {
  universities: UniversityEntity[];
  selectedUniversity: UniversityEntity | null;
}

@Injectable({
  providedIn: 'root'
})
export class UniversitiesService {

  private readonly bffClient: BffClient = inject(BffClient);

  private readonly universities$$ = signalState<UniversityStore>({
    universities: [],
    selectedUniversity: null
  })

  readonly universities$ = toObservable(this.universities$$.universities);

  readonly selectedUniversity$ = toObservable(this.universities$$.selectedUniversity);

  loadUniversities(): Observable<undefined> {
    return this.bffClient.universities().pipe(
      tap(resp => console.log(resp.detail)),
      map(resp => resp.data?.map(universityToEntity)),
      tap(universities => patchState(this.universities$$, {
        universities,
      })),
      switchMap(() => EMPTY)
    )
  }

  selectUniversity(university: UniversityEntity | null): void {
    patchState(this.universities$$, {
      selectedUniversity: university
    })
  }
}

const universityToEntity = (university: University): UniversityEntity => ({
  id: university.id,
  name: university.name ?? "<Неизвестный университет>",
});
