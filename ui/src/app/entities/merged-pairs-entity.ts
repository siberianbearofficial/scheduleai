import {Moment} from 'moment';
import {PairEntity} from './pair-entity';

export interface MergedPairEntity {
  start_time: Moment;
  end_time: Moment;
  act_type: string;
  discipline: string;
  rooms: string[];

  convenience: number;
  wait_time_sec: number;
  status: 'beforePairs' | 'afterPairs' | 'inGap' | 'collision';
  collisions: PairEntity[];
}
