import { Pipe, PipeTransform } from '@angular/core';
import {Duration} from 'moment';

@Pipe({
  standalone: true,
  name: 'durationLabel',
})
export class DurationLabelPipe implements PipeTransform {

  transform(value: Duration): string {
    return `${value.hours() > 0 ? value.hours() + "ч" : ''} ${value.minutes() > 0 ? value.minutes() + "м" : ''}`.trim();
  }

}
