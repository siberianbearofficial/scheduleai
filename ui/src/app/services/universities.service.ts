import { Injectable } from '@angular/core';
import {signalState} from '@ngrx/signals';
import {toObservable} from '@angular/core/rxjs-interop';
import {BehaviorSubject, map, Observable} from 'rxjs';
import {UniversityEntity} from '../entities/university-entity';

@Injectable({
  providedIn: 'root'
})
export class UniversitiesService {

    private readonly universities$$ = signalState<UniversityEntity[]>([
    {
      id: "6",
      name: "МГТУ им. Баумана",
    },
  ])

  readonly universities$ = toObservable(this.universities$$);

  private readonly selectedUniversity$$ = new BehaviorSubject<UniversityEntity | null>(null);

  readonly selectedUniversity$ = this.selectedUniversity$$.pipe();

  findUniversities(query: string | null): Observable<readonly UniversityEntity[] | null> {
    return this.universities$.pipe(
      map(universities => universities.filter(group => group.name == query)),
    )
  }
}
