import {Component, inject, input, InputSignal, OnInit} from '@angular/core';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import {ActivatedRoute, Router} from '@angular/router';
import {MergedPairsService} from '../../services/merged-pairs.service';
import {PairComponent} from '../../components/pair/pair.component';
import {AsyncPipe} from '@angular/common';
import {TuiButton} from '@taiga-ui/core';

@Component({
  selector: 'app-teacher-page',
  imports: [
    LogoComponent,
    FooterComponent,
    PairComponent,
    AsyncPipe,
    TuiButton
  ],
  templateUrl: './teacher-page.component.html',
  styleUrl: './teacher-page.component.scss'
})
export class TeacherPageComponent implements OnInit {
  teacherId: string | null = null;

  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.teacherId = params['teacherId'];
      console.log('Teacher ID:', this.teacherId);
      // Можно сделать запрос на сервер с этим ID, например, получить информацию о преподавателе
    });
  }

  goBack(): void {
    this.router.navigate(['/teacherSchedule']);
  }

  private readonly mergedPairsService: MergedPairsService = inject(MergedPairsService);

  readonly mergedPairs$ = this.mergedPairsService.merged_pairs$
}
