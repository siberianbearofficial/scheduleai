import {Injectable} from '@angular/core';
import {signalState} from '@ngrx/signals';
import {toObservable} from '@angular/core/rxjs-interop';
import {BehaviorSubject, map, Observable} from 'rxjs';
import {UniversityEntity} from '../entities/university-entity';

interface UniversityStore {
  universities: UniversityEntity[];
  selectedUniversity: UniversityEntity | null;
}

@Injectable({
  providedIn: 'root'
})
export class UniversitiesService {

  private readonly universities$$ = signalState<UniversityStore>({
    universities: [
      {
        id: "6",
        name: "МГТУ им. Баумана",
      },
    ],
    selectedUniversity: null
  })

  readonly universities$ = toObservable(this.universities$$).pipe(
    map(store => store.universities),
  );

  readonly selectedUniversity$ = toObservable(this.universities$$).pipe(
    map(store => store.selectedUniversity),
  );

  findUniversities(query: string | null): Observable<readonly UniversityEntity[] | null> {
    return this.universities$.pipe(
      map(universities => {
        if (query === null)
          return universities;
        return universities.filter(group => group.name.includes(query))
      }),
    )
  }
}
