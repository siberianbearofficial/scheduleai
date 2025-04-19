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
  exportAs: "app-group-selector",
  selector: 'app-group-selector',
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
  templateUrl: './group-selector.component.html',
  styleUrls: ['./group-selector.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [TeacherService],
})
export default class GroupSelectorComponent {
  protected readonly service = inject(TeacherService);

  protected search: string | null = '';

  protected readonly control = new FormControl();
}
