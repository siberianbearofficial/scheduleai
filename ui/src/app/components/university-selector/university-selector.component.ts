import {AsyncPipe, NgForOf, NgIf} from '@angular/common';
import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {TuiLet} from '@taiga-ui/cdk';
import {TuiDataList, TuiInitialsPipe, TuiLoader} from '@taiga-ui/core';
import {TuiAvatar} from '@taiga-ui/kit';
import {TuiComboBoxModule, TuiTextfieldControllerModule} from '@taiga-ui/legacy';

import {databaseMockData} from '../../services/database.service';
import {TeacherService} from '../../services/teachers.service';

@Component({
  standalone: true,
  exportAs: "app-university-selector",
  selector: 'app-university-selector',
  imports: [
    AsyncPipe,
    NgForOf,
    NgIf,
    ReactiveFormsModule,
    TuiAvatar,
    TuiComboBoxModule,
    TuiDataList,
    TuiInitialsPipe,
    TuiLet,
    TuiLoader,
    TuiTextfieldControllerModule,
  ],
  templateUrl: './university-selector.component.html',
  styleUrls: ['./university-selector.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [TeacherService],
})
export default class UniversitySelectorComponent {
  protected readonly service = inject(TeacherService);

  protected search: string | null = '';

  protected readonly control = new FormControl();
}
