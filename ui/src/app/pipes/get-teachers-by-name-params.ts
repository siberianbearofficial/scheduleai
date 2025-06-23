import { Pipe, PipeTransform } from '@angular/core';
import {parseJson} from '@angular/cli/src/utilities/json-file';

export interface GetTeachersByNameParams {
  substring: string;
}

@Pipe({
  standalone: true,
  name: 'getTeachersByNameParams'
})
export class GetTeachersByNameParamsPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): GetTeachersByNameParams {
    return parseJson(value) as GetTeachersByNameParams;
  }

}
