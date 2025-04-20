import {AsyncPipe, NgForOf, NgIf} from '@angular/common';
import {ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit} from '@angular/core';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {TuiLet} from '@taiga-ui/cdk';
import {TuiDataList, TuiInitialsPipe, TuiLoader} from '@taiga-ui/core';
import {TuiAvatar, TuiDataListWrapperComponent, TuiFilterByInputPipe, TuiStringifyContentPipe} from '@taiga-ui/kit';
import {TuiComboBoxModule, TuiTextfieldControllerModule} from '@taiga-ui/legacy';

import {UniversitiesService} from '../../services/universities.service';
import {BehaviorSubject, combineLatest, map, Observable, tap} from 'rxjs';
import {UniversityEntity} from '../../entities/university-entity';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';

@Component({
  standalone: true,
  exportAs: "app-university-selector",
  selector: 'app-university-selector',
  imports: [
    AsyncPipe,
    NgForOf,
    NgIf,
    ReactiveFormsModule,
    TuiAvatar,
    TuiComboBoxModule,
    TuiDataList,
    TuiInitialsPipe,
    TuiLet,
    TuiLoader,
    TuiTextfieldControllerModule,
    TuiDataListWrapperComponent,
    TuiStringifyContentPipe,
    TuiFilterByInputPipe,
  ],
  templateUrl: './university-selector.component.html',
  styleUrls: ['./university-selector.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class UniversitySelectorComponent implements OnInit {
  private readonly service = inject(UniversitiesService);
  private readonly destroyRef: DestroyRef = inject(DestroyRef)

  protected readonly control = new FormControl<UniversityEntity | null>(null);

  protected readonly search$ = new BehaviorSubject<string | null>(null);

  protected readonly filtered$: Observable<UniversityEntity[]> = combineLatest([this.search$, this.service.universities$]).pipe(
    map(([search, universities]) => universities.filter(e => e.name.includes(search ?? "")))
  )

  ngOnInit() {
    this.control.valueChanges.pipe(
      tap(u => this.service.selectUniversity(u)),
      takeUntilDestroyed(this.destroyRef)
    ).subscribe();
  }

  protected readonly stringify = (item: UniversityEntity): string => item.name;
}
