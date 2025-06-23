import { Pipe, PipeTransform } from '@angular/core';

export interface GetTeachersByNameParams {
  readonly substring: string;
}

@Pipe({
  standalone: true,
  name: 'getTeachersByNameParams'
})
export class GetTeachersByNameParamsPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): GetTeachersByNameParams {
    return JSON.parse(value) as GetTeachersByNameParams;
  }

}
