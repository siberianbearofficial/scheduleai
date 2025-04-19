import {Moment} from 'moment/moment';

export interface PairEntity {
  group_id: string;
  teachers: string[];
  start_time: Moment;
  end_time: Moment;
  act_type: string;
  discipline: string;
  rooms: string[];
}
