import {Component, Input} from '@angular/core';
import {TuiAvatar, TuiBadge, TuiStatus} from '@taiga-ui/kit'
import {MergedPairEntity} from '../../entities/merged-pairs-entity';
import {AsyncPipe, DatePipe} from '@angular/common';
import {TuiCardLarge, TuiHeader} from '@taiga-ui/layout';
import {TuiSurface, TuiTitle} from '@taiga-ui/core';
import * as moment from 'moment';
import 'moment/locale/ru';

@Component({
  selector: 'app-pair',
  imports: [
    TuiBadge,
    TuiStatus,
    AsyncPipe,
    TuiAvatar,
    TuiCardLarge,
    TuiHeader,
    TuiSurface,
    TuiTitle,
    DatePipe
  ],
  templateUrl: './pair.component.html',
  styleUrl: './pair.component.scss'
})
export class PairComponent {
  @Input() pair!: MergedPairEntity;

  constructor() {
    moment.locale('ru'); // Устанавливаем русскую локаль
  }

  formatDate(date: moment.Moment): string {
    const formattedDate = date.locale('ru').format('dddd, HH:mm');
    return formattedDate.charAt(0).toUpperCase() + formattedDate.slice(1);
  }
}
