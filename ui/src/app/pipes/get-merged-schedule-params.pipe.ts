import { Pipe, PipeTransform } from '@angular/core';
import {parseJson} from '@angular/cli/src/utilities/json-file';
import {Moment} from 'moment';

export interface GetMergedScheduleParams {
  group: string;
  teacherId: string;
  from: Moment;
  to: Moment;
}

@Pipe({
  standalone: true,
  name: 'getMergedScheduleParams'
})
export class GetMergedScheduleParamsPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): GetMergedScheduleParams {
    return parseJson(value) as GetMergedScheduleParams;
  }

}
