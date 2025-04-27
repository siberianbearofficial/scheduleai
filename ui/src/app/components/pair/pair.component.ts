import {ChangeDetectionStrategy, Component, Input} from '@angular/core';
import {TuiBadge, TuiStatus} from '@taiga-ui/kit'
import {TuiCardLarge, TuiCardMedium, TuiHeader} from '@taiga-ui/layout';
import {TuiHint, TuiSurface, TuiTitle} from '@taiga-ui/core';
import moment from 'moment';
import 'moment/locale/ru';
import {DurationLabelPipe} from '../../pipes/duration-label.pipe';
import {PairEntity, MergedPairStatus} from '../../entities/pair-entity';

@Component({
  selector: 'app-pair',
  imports: [
    TuiBadge,
    TuiStatus,
    TuiCardLarge,
    TuiHeader,
    TuiSurface,
    TuiTitle,
    TuiHint,
    DurationLabelPipe,
    TuiCardMedium
  ],
  templateUrl: './pair.component.html',
  standalone: true,
  styleUrl: './pair.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PairComponent {
  @Input() pair!: PairEntity;

  constructor() {
    moment.locale('ru'); // Устанавливаем русскую локаль
  }

  formatDate(date: moment.Moment): string {
    const formattedDate = date.locale('ru').format('dddd, HH:mm');
    return formattedDate.charAt(0).toUpperCase() + formattedDate.slice(1);
  }

  getAppearance(pair: PairEntity): string {
    if (pair.convenience == null)
      return "primary";
    if (pair.convenience.status == MergedPairStatus.Collision)
      return "negative";
    if (pair.convenience.coefficient >= 0.8)
      return "positive";
    if (pair.convenience.coefficient >= 0.5)
      return "primary";
    return "warning";
  }

  protected readonly MergedPairStatus = MergedPairStatus;

  protected humanizeActType(actType: string): string {
    return actTypeMap[actType] || actType;
  }
}

const actTypeMap: Record<string, string> = {
  "lecture": "Лекция",
  "seminar": "Семинар",
  "lab": "Лабораторная",
} as const;
