import {inject, Injectable} from '@angular/core';
import {patchState, signalState} from '@ngrx/signals';
import {toObservable} from '@angular/core/rxjs-interop';
import {EMPTY, map, Observable, switchMap, tap} from 'rxjs';
import {UniversityEntity} from '../entities/university-entity';
import {BffClient, University} from '../bff-client/bff-client';
import {LoadingStatus} from '../entities/LoadingStatus';

interface UniversityStore {
  universities: UniversityEntity[];
  selectedUniversity: UniversityEntity | null;
  loadingUniversitiesStatus: LoadingStatus;
  loadingSelectedStatus: LoadingStatus;
}

const selectedUniversityKey = "scheduleai-selectedUniversity";

@Injectable({
  providedIn: 'root'
})
export class UniversitiesService {

  private readonly bffClient: BffClient = inject(BffClient);

  private readonly universities$$ = signalState<UniversityStore>({
    universities: [],
    selectedUniversity: null,
    loadingUniversitiesStatus: LoadingStatus.NotStarted,
    loadingSelectedStatus: LoadingStatus.NotStarted,
  })

  readonly universities$ = toObservable(this.universities$$.universities);

  readonly selectedUniversity$ = toObservable(this.universities$$.selectedUniversity);

  loadUniversities(): Observable<undefined> {
    patchState(this.universities$$, {loadingUniversitiesStatus: LoadingStatus.InProgress});
    return this.bffClient.universities().pipe(
      tap(resp => console.log(resp.detail)),
      map(resp => resp.data?.map(universityToEntity)),
      tap(universities => patchState(this.universities$$, {
        universities,
      })),
      tap(() => patchState(this.universities$$, {loadingUniversitiesStatus: LoadingStatus.Completed}),),
      tap(universities => {
        const selectedUniversityId = localStorage.getItem(selectedUniversityKey);
        universities = universities?.filter(u => u.id === selectedUniversityId);
        if (universities == undefined || universities.length != 1) {
          patchState(this.universities$$, {selectedUniversity: null, loadingSelectedStatus: LoadingStatus.Failed})
        } else {
          patchState(this.universities$$, {
            selectedUniversity: universities[0],
            loadingSelectedStatus: LoadingStatus.Completed
          });
        }
      }),
      switchMap(() => EMPTY)
    )
  }

  readonly saveSelectedUniversity$: Observable<undefined> = toObservable(this.universities$$).pipe(
    tap(store => {
      if (store.loadingUniversitiesStatus == LoadingStatus.Completed) {
        if (store.selectedUniversity === null) {
          localStorage.removeItem(selectedUniversityKey);
        } else {
          localStorage.setItem(selectedUniversityKey, store.selectedUniversity.id);
        }
      }
    }),
    switchMap(() => EMPTY),
  )

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
