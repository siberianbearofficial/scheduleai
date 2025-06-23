import { Pipe, PipeTransform } from '@angular/core';
import {Moment} from 'moment';

export interface GetTeacherScheduleParams {
  readonly teacherId: string;
  readonly from: Moment;
  readonly to: Moment;
}

@Pipe({
  standalone: true,
  name: 'getTeacherScheduleParams'
})
export class GetTeacherScheduleParamsPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): GetTeacherScheduleParams {
    return JSON.parse(value) as GetTeacherScheduleParams;
  }

}
