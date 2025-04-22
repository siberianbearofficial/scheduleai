import {Moment} from 'moment/moment';

export interface PairEntity {
  groups: string[];
  teachers: string[];
  startTime: Moment;
  endTime: Moment;
  actType: string;
  discipline: string;
  rooms: string[];
}
