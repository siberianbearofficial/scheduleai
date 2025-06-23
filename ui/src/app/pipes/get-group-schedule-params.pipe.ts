import { Pipe, PipeTransform } from '@angular/core';
import {Moment} from 'moment';

export interface GetGroupScheduleParams {
  readonly group: string;
  readonly from: Moment;
  readonly to: Moment;
}

@Pipe({
  standalone: true,
  name: 'getGroupScheduleParams'
})
export class GetGroupScheduleParamsPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): GetGroupScheduleParams {
    return JSON.parse(value) as GetGroupScheduleParams;
  }

}
