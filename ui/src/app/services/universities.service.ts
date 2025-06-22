import {inject, Injectable} from '@angular/core';
import {patchState, signalState} from '@ngrx/signals';
import {toObservable} from '@angular/core/rxjs-interop';
import {EMPTY, map, Observable, switchMap, tap} from 'rxjs';
import {UniversityEntity} from '../entities/university-entity';
import {BffClient, IUniversity} from '../bff-client/bff-client';
import {LoadingStatus} from '../entities/LoadingStatus';
import {StorageService} from './storage.service';

interface UniversityStore {
  readonly universities: UniversityEntity[];
  readonly selectedUniversity: UniversityEntity | null;
  readonly loadingUniversitiesStatus: LoadingStatus;
  readonly loadingSelectedStatus: LoadingStatus;
}

@Injectable({
  providedIn: 'root'
})
export class UniversitiesService {
  private readonly bffClient: BffClient = inject(BffClient);
  private readonly storageService: StorageService = inject(StorageService);

  private readonly universities$$ = signalState<UniversityStore>({
    universities: [],
    selectedUniversity: null,
    loadingUniversitiesStatus: LoadingStatus.NotStarted,
    loadingSelectedStatus: LoadingStatus.NotStarted,
  })

  readonly universitiesInfo$ = toObservable(this.universities$$);
  readonly universities$ = toObservable(this.universities$$.universities);

  readonly selectedUniversity$ = toObservable(this.universities$$.selectedUniversity);

  loadUniversities(): Observable<undefined> {
    patchState(this.universities$$, {loadingUniversitiesStatus: LoadingStatus.InProgress});
    return this.bffClient.universities().pipe(
      tap(resp => console.log(resp.detail)),
      map(resp => resp.data?.map(universityToEntity)),
      tap(universities => patchState(this.universities$$, {
        universities,
        loadingUniversitiesStatus: LoadingStatus.Completed,
        selectedUniversity: this.storageService.getUniversity(universities ?? []),
        loadingSelectedStatus: LoadingStatus.Completed,
      })),
      switchMap(() => EMPTY)
    )
  }

  readonly saveSelectedUniversity$: Observable<undefined> = toObservable(this.universities$$).pipe(
    tap(store => {
      if (store.loadingSelectedStatus != LoadingStatus.NotStarted) {
        this.storageService.setUniversity(store.selectedUniversity);
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

const universityToEntity = (university: IUniversity): UniversityEntity => ({
  id: university.id ?? "",
  name: university.name ?? "<Неизвестный университет>",
});
