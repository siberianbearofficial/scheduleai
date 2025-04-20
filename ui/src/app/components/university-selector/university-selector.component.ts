import {AsyncPipe, NgForOf, NgIf} from '@angular/common';
import {ChangeDetectionStrategy, Component, inject, OnInit} from '@angular/core';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {TuiLet} from '@taiga-ui/cdk';
import {TuiDataList, TuiInitialsPipe, TuiLoader} from '@taiga-ui/core';
import {TuiAvatar} from '@taiga-ui/kit';
import {TuiComboBoxModule, TuiTextfieldControllerModule} from '@taiga-ui/legacy';

import {UniversitiesService} from '../../services/universities.service';
import {BehaviorSubject, combineLatest, map, Observable, tap} from 'rxjs';
import {UniversityEntity} from '../../entities/university-entity';

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
  ],
  templateUrl: './university-selector.component.html',
  styleUrls: ['./university-selector.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class UniversitySelectorComponent implements OnInit {
  private readonly service = inject(UniversitiesService);

  protected readonly control = new FormControl<UniversityEntity | null>(null);

  protected readonly search$ = new BehaviorSubject<string | null>(null);

  protected readonly filtered$: Observable<UniversityEntity[]> = combineLatest([this.search$, this.service.universities$]).pipe(
    map(([search, universities]) => universities.filter(e => e.name.includes(search ?? "")))
  )

  ngOnInit() {
    this.control.valueChanges.pipe(
      tap(u => this.service.selectUniversity(u))
    )
  }
}
