import {Moment} from 'moment';
import {PairEntity} from './pair-entity';

export enum MergedPairStatus {
  beforePairs ,
  afterPairs ,
  inGap,
  collision
}

export interface MergedPairEntity {
  startTime: Moment;
  endTime: Moment;
  actType: string;
  discipline: string;
  rooms: string[];

  convenience: number;
  waitTimeSec: number;
  status: MergedPairStatus;
  collisions: PairEntity[];
}
