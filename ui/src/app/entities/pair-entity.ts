import {Moment} from 'moment/moment';

export interface PairEntity {
  readonly groups: string[];
  readonly teachers: string[];
  readonly startTime: Moment;
  readonly endTime: Moment;
  readonly actType: string;
  readonly discipline: string;
  readonly rooms: string[];
}
