import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import {RouterLink} from '@angular/router';
import {AsyncPipe} from '@angular/common';
import {TuiAppearance, TuiTextfieldComponent, TuiTextfieldDirective} from '@taiga-ui/core';
import {combineLatest, map, Observable} from 'rxjs';
import {TeacherService} from '../../services/teachers.service';
import {TeacherCardComponent} from '../../components/teacher-card/teacher-card.component';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {TeacherEntity} from '../../entities/teacher-entity';

@Component({
  standalone: true,
  selector: 'app-teacher-page',
  imports: [
    LogoComponent,
    FooterComponent,
    AsyncPipe,
    TuiAppearance,
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
