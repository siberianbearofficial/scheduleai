import {ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit} from '@angular/core';
import {AsyncPipe} from '@angular/common';
import {MergedPairsService} from '../../services/merged-pairs.service';
import {BehaviorSubject, EMPTY, Observable, switchMap} from 'rxjs';
import {TeacherService} from '../../services/teachers.service';
import {PairComponent} from '../../components/pair/pair.component';
import {TuiCardLarge, TuiHeader} from '@taiga-ui/layout';
import {TuiAppearance, TuiTitle} from '@taiga-ui/core';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {PairEntity} from '../../entities/pair-entity';
import {HeaderComponent} from '../../components/header/header.component';

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
    HeaderComponent
  ],
  templateUrl: './merged-pairs-page.component.html',
  styleUrl: './merged-pairs-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MergedPairsPageComponent implements OnInit {
  private readonly mergedPairsService: MergedPairsService = inject(MergedPairsService);
  private readonly teacherService: TeacherService = inject(TeacherService);
  private readonly destroyRef: DestroyRef = inject(DestroyRef);

  readonly mergedPairs$: Observable<PairEntity[]> = this.mergedPairsService.mergedPairs$;
  private readonly teacherId$$ = new BehaviorSubject<string | null>(null);
  protected readonly teacher$ = this.teacherId$$.pipe(
    switchMap(id => id ? this.teacherService.teacherById(id) : EMPTY)
  );

  ngOnInit() {
    this.mergedPairsService.loadMergedPairsOnUniversityChange$.pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe();
  }
}
