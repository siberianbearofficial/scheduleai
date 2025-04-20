import {Component, inject} from '@angular/core';
import {TeacherCardComponent} from '../../components/teacher-card/teacher-card.component';
import {AsyncPipe} from '@angular/common';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import TeacherSearchBarComponent from '../../components/teacher-search-bar/teacher-search-bar.component';
import {RouterLink} from '@angular/router';
import {MergedPairsService} from '../../services/merged-pairs.service';

@Component({
  selector: 'app-merged-pairs-page',
  standalone: true,
  imports: [
    TeacherCardComponent,
    AsyncPipe,
    LogoComponent,
    FooterComponent,
    TeacherSearchBarComponent,
    RouterLink
  ],
  templateUrl: './merged-pairs-page.component.html',
  styleUrl: './merged-pairs-page.component.scss'
})
export class MergedPairsPageComponent {
  private readonly mergedPairsService: MergedPairsService = inject(MergedPairsService);

  readonly mergedPairs$ = this.mergedPairsService.mergedPairs$;
}
