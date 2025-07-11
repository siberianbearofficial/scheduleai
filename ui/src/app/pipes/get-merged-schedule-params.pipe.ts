import { Pipe, PipeTransform } from '@angular/core';
import {Moment} from 'moment';

export interface GetMergedScheduleParams {
  readonly group: string;
  readonly teacherId: string;
  readonly from: Moment;
  readonly to: Moment;
}

@Pipe({
  standalone: true,
  name: 'getMergedScheduleParams'
})
export class GetMergedScheduleParamsPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): GetMergedScheduleParams {
    return JSON.parse(value) as GetMergedScheduleParams;
  }

}
