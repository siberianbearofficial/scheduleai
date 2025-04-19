import {Component, inject, OnInit} from '@angular/core';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import {ActivatedRoute} from '@angular/router';
import {MergedPairsService} from '../../services/merged-pairs.service';
import {PairComponent} from '../../components/pair/pair.component';
import {AsyncPipe} from '@angular/common';

@Component({
  selector: 'app-teacher-page',
  imports: [
    LogoComponent,
    FooterComponent,
    PairComponent,
    AsyncPipe
  ],
  templateUrl: './teacher-page.component.html',
  styleUrl: './teacher-page.component.scss'
})
export class TeacherPageComponent implements OnInit {
  teacherId: string | null = null;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.teacherId = params['teacherId'];
      console.log('Teacher ID:', this.teacherId);
      // Можно сделать запрос на сервер с этим ID, например, получить информацию о преподавателе
    });
  }

  private readonly mergedPairsService: MergedPairsService = inject(MergedPairsService);

  readonly mergedPairs$ = this.mergedPairsService.merged_pairs$
}
