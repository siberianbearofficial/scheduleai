import {ChangeDetectionStrategy, Component, inject, input, InputSignal} from '@angular/core';
import {toObservable} from '@angular/core/rxjs-interop';
import {switchMap} from 'rxjs';
import {AsyncPipe} from '@angular/common';
import {TuiAppearance, TuiSurface, TuiTitle} from '@taiga-ui/core';
import {TuiAvatar} from '@taiga-ui/kit';
import {TuiCardLarge, TuiHeader} from '@taiga-ui/layout';
import {TeacherService} from '../../services/teachers.service';

@Component({
  selector: 'app-teacher-card',
  imports: [
    AsyncPipe,
    TuiAppearance,
    TuiAvatar,
    TuiCardLarge,
    TuiHeader,
    TuiSurface,
    TuiTitle,
  ],
  templateUrl: './teacher-card.component.html',
  standalone: true,
  styleUrl: './teacher-card.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class TeacherCardComponent {
  readonly teacherService = inject(TeacherService);

  readonly teacherId: InputSignal<string> = input('');

  readonly teacher$ = toObservable(this.teacherId).pipe(
    switchMap(id => this.teacherService.teacherById(id))
  );
}
