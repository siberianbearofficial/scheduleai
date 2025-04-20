import {Moment} from 'moment';
import {PairEntity} from './pair-entity';

export interface MergedPairEntity {
  startTime: Moment;
  endTime: Moment;
  actType: string;
  discipline: string;
  rooms: string[];

  convenience: number;
  waitTimeSec: number;
  status: 'beforePairs' | 'afterPairs' | 'inGap' | 'collision';
  collisions: PairEntity[];
}
