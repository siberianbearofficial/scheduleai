import {Component, inject, OnInit} from '@angular/core';
import {TeacherCardComponent} from '../../components/teacher-card/teacher-card.component';
import {AsyncPipe} from '@angular/common';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import TeacherSearchBarComponent from '../../components/teacher-search-bar/teacher-search-bar.component';
import {ActivatedRoute, Route, Router, RouterLink} from '@angular/router';
import {MergedPairsService} from '../../services/merged-pairs.service';
import {BehaviorSubject, EMPTY, switchMap} from 'rxjs';
import {TeacherService} from '../../services/teachers.service';
import {PairComponent} from '../../components/pair/pair.component';
import {TuiCardLarge, TuiHeader} from '@taiga-ui/layout';
import {TuiAppearance, TuiButton, TuiTitle} from '@taiga-ui/core';

@Component({
  selector: 'app-merged-pairs-page',
  standalone: true,
  imports: [
    TeacherCardComponent,
    AsyncPipe,
    LogoComponent,
    FooterComponent,
    TeacherSearchBarComponent,
    RouterLink,
    PairComponent,
    TuiHeader,
    TuiAppearance,
    TuiCardLarge,
    TuiTitle,
    TuiButton
  ],
  templateUrl: './merged-pairs-page.component.html',
  styleUrl: './merged-pairs-page.component.scss'
})
export class MergedPairsPageComponent implements OnInit {
  private readonly mergedPairsService: MergedPairsService = inject(MergedPairsService);
  private readonly teacherService: TeacherService = inject(TeacherService);
  private readonly route: ActivatedRoute = inject(ActivatedRoute);
  private readonly router = inject(Router);

  readonly mergedPairs$ = this.mergedPairsService.mergedPairs$;
  private readonly teacherId$$ = new BehaviorSubject<string | null>(null);
  protected readonly teacher$ = this.teacherId$$.pipe(
    switchMap(id => id ? this.teacherService.teacherById(id) : EMPTY)
  );
  teacherId: string | null = null;

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.teacherId = params['teacherId'];
      console.log('Teacher ID:', this.teacherId);
      // Можно сделать запрос на сервер с этим ID, например, получить информацию о преподавателе
    });
  }

  goBack(): void {
    this.router.navigate(['/teacherSchedule']);
  }
}
