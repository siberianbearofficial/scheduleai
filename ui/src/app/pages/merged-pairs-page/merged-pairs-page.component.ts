import {ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit} from '@angular/core';
import {AsyncPipe} from '@angular/common';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import {Router} from '@angular/router';
import {MergedPairsService} from '../../services/merged-pairs.service';
import {BehaviorSubject, EMPTY, Observable, switchMap} from 'rxjs';
import {TeacherService} from '../../services/teachers.service';
import {PairComponent} from '../../components/pair/pair.component';
import {TuiCardLarge, TuiHeader} from '@taiga-ui/layout';
import {TuiAppearance, TuiButton, TuiTitle} from '@taiga-ui/core';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {PairEntity} from '../../entities/pair-entity';

@Component({
  selector: 'app-merged-pairs-page',
  standalone: true,
  imports: [
    AsyncPipe,
    LogoComponent,
    FooterComponent,
    PairComponent,
    TuiHeader,
    TuiAppearance,
    TuiCardLarge,
    TuiTitle,
    TuiButton
  ],
  templateUrl: './merged-pairs-page.component.html',
  styleUrl: './merged-pairs-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MergedPairsPageComponent implements OnInit {
  private readonly mergedPairsService: MergedPairsService = inject(MergedPairsService);
  private readonly teacherService: TeacherService = inject(TeacherService);
  private readonly router = inject(Router);
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

  goBack(): void {
    this.router.navigate(['/teacherSchedule']);
  }
}
