import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  standalone: true,
  name: 'humanizedActType'
})
export class HumanizedActTypePipe implements PipeTransform {

  transform(value: string): string {
    return actTypeMap[value] || value;
  }

}

const actTypeMap: Record<string, string> = {
  "lecture": "Лекция",
  "seminar": "Семинар",
  "lab": "Лабораторная",
} as const;

