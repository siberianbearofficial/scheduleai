import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {TuiButton, TuiDialogContext} from '@taiga-ui/core';
import {injectContext} from '@taiga-ui/polymorpheus';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {TeacherService} from '../../services/teachers.service';
import {combineLatest, map} from 'rxjs';
import {TeacherCardComponent} from '../teacher-card/teacher-card.component';
import {Router, RouterLink} from '@angular/router';
import {AsyncPipe} from '@angular/common';
import {TeacherEntity} from '../../entities/teacher-entity';
import {TuiAutoFocus} from '@taiga-ui/cdk';
import {TuiInputInline} from '@taiga-ui/kit';

@Component({
  selector: 'app-mobile-search-dialog',
  imports: [
    TuiButton,
    TeacherCardComponent,
    RouterLink,
    AsyncPipe,
    ReactiveFormsModule,
    TuiAutoFocus,
    TuiInputInline
  ],
  templateUrl: './mobile-search-dialog.component.html',
  standalone: true,
  styleUrl: './mobile-search-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MobileSearchDialogComponent {
  readonly context = injectContext<TuiDialogContext>();
  private readonly router = inject(Router);

  private readonly teacherService = inject(TeacherService);
  protected readonly control = new FormControl<string>("");

  protected readonly filtered$ = combineLatest(this.control.valueChanges, this.teacherService.teachers$).pipe(
    map(([search, teachers]) => teachers.filter(e => e.fullName.includes(search ?? "")))
  );

  close() {
    this.context.completeWith();
  }

  protected selectTeacher(teacher: TeacherEntity) {
    this.teacherService.selectTeacher(teacher);
    this.close();
  }

  onSearch() {
    const query = this.control.value;
    if (query?.trim()) {
      void this.router.navigate(['/chat'], {
        queryParams: {message: query},
      });
      this.close();
    }
  }
}
