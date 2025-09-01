import {ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit} from '@angular/core';
import {AsyncPipe} from '@angular/common';
import {MergedPairsService} from '../../services/merged-pairs.service';
import {BehaviorSubject, combineLatest, EMPTY, map, NEVER, Observable, startWith, switchMap, tap} from 'rxjs';
import {TeacherService} from '../../services/teachers.service';
import {PairComponent} from '../../components/pair/pair.component';
import {TuiCardLarge, TuiHeader} from '@taiga-ui/layout';
import {TuiAppearance, TuiTextfield, TuiTitle} from '@taiga-ui/core';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {PairEntity} from '../../entities/pair-entity';
import {HeaderComponent} from '../../components/header/header.component';
import {TuiInputDateRangeModule, TuiMultiSelectModule, TuiTextfieldControllerModule} from '@taiga-ui/legacy';
import {TuiChevron, TuiDataListWrapper, TuiDayRangePeriod, TuiSelect} from '@taiga-ui/kit';
import {TuiDay, TuiDayRange, TuiLet} from '@taiga-ui/cdk';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import moment from 'moment';
import {ActivatedRoute} from '@angular/router';
import {HumanizedActTypePipe} from '../../pipes/humanized-act-type.pipe';

const today = TuiDay.currentLocal();

@Component({
  selector: 'app-merged-pairs-page',
  standalone: true,
  imports: [
    AsyncPipe,
    PairComponent,
    TuiHeader,
    TuiAppearance,
    TuiCardLarge,
    TuiTitle,
    HeaderComponent,
    TuiInputDateRangeModule,
    TuiTextfield,
    ReactiveFormsModule,
    TuiChevron,
    TuiSelect,
    TuiDataListWrapper,
    TuiMultiSelectModule,
    TuiLet,
    HumanizedActTypePipe,
    TuiTextfieldControllerModule,
  ],
  templateUrl: './merged-pairs-page.component.html',
  styleUrl: './merged-pairs-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MergedPairsPageComponent implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly mergedPairsService: MergedPairsService = inject(MergedPairsService);
  private readonly teacherService: TeacherService = inject(TeacherService);
  private readonly destroyRef: DestroyRef = inject(DestroyRef);

  private readonly teacherId$$ = new BehaviorSubject<string | null>(null);
  protected readonly teacher$ = this.teacherId$$.pipe(
    switchMap(id => id ? this.teacherService.teacherById(id) : EMPTY)
  );

  ngOnInit() {
    this.mergedPairsService.loadMergedPairsOnUniversityChange$.pipe(
      takeUntilDestroyed(this.destroyRef),
    ).subscribe();

    this.dateRangeControl.valueChanges.pipe(
      tap(range => this.mergedPairsService.setDateRange(
        moment(range?.from.toLocalNativeDate()),
        moment(range?.to.append({day: 1}).toLocalNativeDate()),
      )),
      takeUntilDestroyed(this.destroyRef),
    ).subscribe();

    combineLatest(
      this.route.params.pipe(
        switchMap(params => {
          const teacherId = params['teacherId'];
          if (teacherId)
            return this.teacherService.teacherById(teacherId);
          return NEVER;
        }),
      ),
      this.teacherService.selectedTeacher$
    ).pipe(
      tap(([newTeacher, selectedTeacher]) => {
        if (newTeacher && newTeacher.id !== selectedTeacher?.id) {
          console.log("Selecting teacher:", newTeacher?.fullName);
          this.teacherService.selectTeacher(newTeacher);
        }
      }),
      takeUntilDestroyed(this.destroyRef),
    )
      .subscribe();
  }

  protected readonly dateRangeControl = new FormControl(new TuiDayRange(today, today.append({day: 6})));

  protected readonly orders = [
    "По времени",
    "По удобству"
  ];

  protected readonly orderControl = new FormControl(this.orders[0]);

  protected readonly actTypes$ = this.mergedPairsService.mergedPairs$.pipe(
    map(pairs => {
      let actTypes: string[] = [];
      for (const pair of pairs) {
        if (!actTypes.includes(pair.actType))
          actTypes.push(pair.actType);
      }
      return actTypes;
    })
  );

  protected readonly actTypesControl = new FormControl<string[]>([]);

  readonly mergedPairs$: Observable<PairEntity[]> = combineLatest(
    this.mergedPairsService.mergedPairs$,
    this.orderControl.valueChanges.pipe(
      startWith(this.orders[0])
    ),
    this.actTypesControl.valueChanges.pipe(
      startWith([] as string[])
    )
  ).pipe(
    map(([mergedPairs, order, actTypes]) => {
      if (actTypes && actTypes.length) {
        console.log("filtering by", actTypes)
        mergedPairs = mergedPairs.filter(p => actTypes.includes(p.actType))
      }

      if (order == "По времени")
        return mergedPairs.sort((a, b) => a.startTime.diff(b.startTime));
      else if (order == "По удобству")
        return mergedPairs.sort((a, b) => (b.convenience?.coefficient ?? 0) - (a.convenience?.coefficient ?? 0));
      else
        return mergedPairs;
    })
  );

  protected readonly datePeriods = [
    new TuiDayRangePeriod(
      new TuiDayRange(today, today),
      'Сегодня',
    ),
    new TuiDayRangePeriod(
      new TuiDayRange(today.append({day: 1}), today.append({day: 1})),
      'Завтра',
    ),
    new TuiDayRangePeriod(
      new TuiDayRange(today, today.append({day: 6})),
      'На этой неделе',
    ),
    new TuiDayRangePeriod(
      new TuiDayRange(today.append({day: 7}), today.append({day: 13})),
      'На следующей неделе',
    ),
  ];
}
