import {Component, inject, input, InputSignal} from '@angular/core';
import {TeacherService} from '../../services/groups.service';
import {toObservable} from '@angular/core/rxjs-interop';
import {switchMap} from 'rxjs';
import {AsyncPipe} from '@angular/common';
import {TuiAppearance, TuiIcon, TuiSurface, TuiTitle} from '@taiga-ui/core';
import {TuiAvatar} from '@taiga-ui/kit';
import {TuiCardLarge, TuiCell, TuiHeader} from '@taiga-ui/layout';

@Component({
  selector: 'app-teacher-card',
  imports: [
    AsyncPipe,
    TuiAppearance,
    TuiAvatar,
    TuiCardLarge,
    TuiCell,
    TuiHeader,
    TuiIcon,
    TuiSurface,
    TuiTitle,
  ],
  templateUrl: './teacher-card.component.html',
  standalone: true,
  styleUrl: './teacher-card.component.less'
})

export class TeacherCardComponent {
  readonly teacherService = inject(TeacherService);

  readonly teacherId: InputSignal<string> = input('');

  readonly teacher$ = toObservable(this.teacherId).pipe(
    switchMap(id => this.teacherService.teacherById(id))
  );
}
