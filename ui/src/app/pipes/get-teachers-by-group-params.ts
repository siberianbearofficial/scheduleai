import { Pipe, PipeTransform } from '@angular/core';

export interface GetTeachersByGroupParams {
  readonly group: string;
}

@Pipe({
  standalone: true,
  name: 'getTeachersByGroupParams'
})
export class GetTeachersByGroupParamsPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): GetTeachersByGroupParams {
    return JSON.parse(value) as GetTeachersByGroupParams;
  }

}
