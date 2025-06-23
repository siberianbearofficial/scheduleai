import {ChangeDetectionStrategy, Component, Input} from '@angular/core';
import {TuiExpandComponent, TuiIcon} from '@taiga-ui/core';
import {ToolCallEntity} from '../../entities/message-entity';
import {TuiAccordion} from '@taiga-ui/kit';
import {GetGroupScheduleParamsPipe} from '../../pipes/get-group-schedule-params.pipe';
import {GetTeachersByGroupParamsPipe} from '../../pipes/get-teachers-by-group-params';
import {GetTeachersByNameParamsPipe} from '../../pipes/get-teachers-by-name-params';
import {GetTeacherScheduleParamsPipe} from '../../pipes/get-teacher-schedule-params.pipe';
import {TuiLet} from '@taiga-ui/cdk';
import {GetMergedScheduleParamsPipe} from '../../pipes/get-merged-schedule-params.pipe';

@Component({
  standalone: true,
  selector: 'app-tool-call',
  imports: [
    TuiExpandComponent,
    TuiAccordion,
    GetTeachersByNameParamsPipe,
    GetGroupScheduleParamsPipe,
    GetTeachersByGroupParamsPipe,
    GetTeacherScheduleParamsPipe,
    TuiLet,
    GetMergedScheduleParamsPipe,
    TuiIcon,
  ],
  templateUrl: './tool-call.component.html',
  styleUrl: './tool-call.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ToolCallComponent {
  @Input() toolCall!: ToolCallEntity;
}
