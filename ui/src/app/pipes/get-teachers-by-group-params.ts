import { Pipe, PipeTransform } from '@angular/core';
import {parseJson} from '@angular/cli/src/utilities/json-file';

export interface GetTeachersByGroupParams {
  readonly group: string;
}

@Pipe({
  standalone: true,
  name: 'getTeachersByGroupParams'
})
export class GetTeachersByGroupParamsPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): GetTeachersByGroupParams {
    return parseJson(value) as GetTeachersByGroupParams;
  }

}
