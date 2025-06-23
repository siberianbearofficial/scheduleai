import { Pipe, PipeTransform } from '@angular/core';
import {parseJson} from '@angular/cli/src/utilities/json-file';
import {Moment} from 'moment';

export interface GetTeacherScheduleParams {
  teacherId: string;
  from: Moment;
  to: Moment;
}

@Pipe({
  standalone: true,
  name: 'getTeacherScheduleParams'
})
export class GetTeacherScheduleParamsPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): GetTeacherScheduleParams {
    return parseJson(value) as GetTeacherScheduleParams;
  }

}
