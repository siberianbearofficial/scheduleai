import {ChangeDetectionStrategy, Component, inject, OnInit} from '@angular/core';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import {ActivatedRoute, Router} from '@angular/router';
import {MergedPairsService} from '../../services/merged-pairs.service';
import {PairComponent} from '../../components/pair/pair.component';
import {AsyncPipe} from '@angular/common';
import {TuiAppearance, TuiButton, TuiTitle} from '@taiga-ui/core';
import {TeacherService} from '../../services/groups.service';
import {BehaviorSubject, EMPTY, switchMap} from 'rxjs';
import {TuiCardLarge, TuiHeader} from '@taiga-ui/layout';
import {TuiBadge} from '@taiga-ui/kit';

@Component({
  standalone: true,
  selector: 'app-teacher-page',
  imports: [
    LogoComponent,
    FooterComponent,
    PairComponent,
    AsyncPipe,
    TuiButton,
    TuiAppearance,
    TuiCardLarge,
    TuiHeader,
    TuiTitle,
    TuiBadge
  ],
  templateUrl: './teacher-page.component.html',
  styleUrl: './teacher-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TeacherPageComponent implements OnInit {
  private readonly teacherService = inject(TeacherService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);

  private readonly teacherId$$ = new BehaviorSubject<string | null>(null);
  protected readonly teacher$ = this.teacherId$$.pipe(
    switchMap(id => id ? this.teacherService.teacherById(id) : EMPTY)
  );

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const teacherId = params['teacherId'] as string | null;
      this.teacherId$$.next(teacherId);
    });
  }

  goBack(): void {
    this.router.navigate(['/teacherSchedule']);
  }

  private readonly mergedPairsService: MergedPairsService = inject(MergedPairsService);

  readonly mergedPairs$ = this.mergedPairsService.merged_pairs$
}
