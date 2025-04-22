import {ChangeDetectionStrategy, Component, DestroyRef, inject} from '@angular/core';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import {RouterLink} from '@angular/router';
import {PairComponent} from '../../components/pair/pair.component';
import {AsyncPipe} from '@angular/common';
import {TuiAppearance, TuiButton, TuiTextfieldComponent, TuiTextfieldDirective, TuiTitle} from '@taiga-ui/core';
import {combineLatest, map, Observable} from 'rxjs';
import {TuiCardLarge, TuiHeader} from '@taiga-ui/layout';
import {TuiBadge} from '@taiga-ui/kit';
import {TeacherService} from '../../services/teachers.service';
import TeacherSearchBarComponent from '../../components/teacher-search-bar/teacher-search-bar.component';
import {TeacherCardComponent} from '../../components/teacher-card/teacher-card.component';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {TeacherEntity} from '../../entities/teacher-entity';

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
    TeacherCardComponent,
    ReactiveFormsModule,
    TuiTextfieldComponent,
    TuiTextfieldDirective
  ],
  templateUrl: './teacher-page.component.html',
  styleUrl: './teacher-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TeacherPageComponent {
  private readonly teacherService: TeacherService = inject(TeacherService);

  protected readonly searchControl = new FormControl<string | null>(null);

  protected readonly filtered$: Observable<TeacherEntity[]> = combineLatest([this.searchControl.valueChanges, this.teacherService.teachers$]).pipe(
    map(([search, universities]) => universities.filter(e => e.fullName.includes(search ?? "")))
  );

  protected selectTeacher(teacher: TeacherEntity) {
    this.teacherService.selectTeacher(teacher);
  }

}
