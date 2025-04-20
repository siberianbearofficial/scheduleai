import {ChangeDetectionStrategy, Component, inject, OnInit} from '@angular/core';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {MergedPairsService} from '../../services/merged-pairs.service';
import {PairComponent} from '../../components/pair/pair.component';
import {AsyncPipe} from '@angular/common';
import {TuiAppearance, TuiButton, TuiTitle} from '@taiga-ui/core';
import {BehaviorSubject, EMPTY, switchMap} from 'rxjs';
import {TuiCardLarge, TuiHeader} from '@taiga-ui/layout';
import {TuiBadge} from '@taiga-ui/kit';
import {TeacherService} from '../../services/teachers.service';
import TeacherSearchBarComponent from '../../components/teacher-search-bar/teacher-search-bar.component';
import {TeacherCardComponent} from '../../components/teacher-card/teacher-card.component';

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
    TuiBadge,
    TeacherSearchBarComponent,
    RouterLink,
    TeacherCardComponent
  ],
  templateUrl: './teacher-page.component.html',
  styleUrl: './teacher-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TeacherPageComponent {
  private readonly teacherService: TeacherService = inject(TeacherService);


  readonly teachers$ = this.teacherService.teachers$;
}
