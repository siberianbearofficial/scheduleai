import {Duration, Moment} from 'moment';
import {PairEntity} from './pair-entity';

export enum MergedPairStatus {
  BeforePairs ,
  AfterPairs ,
  InGap,
  Collision
}

export interface MergedPairEntity {
  readonly startTime: Moment;
  readonly endTime: Moment;
  readonly actType: string;
  readonly discipline: string;
  readonly rooms: string[];
  readonly convenience: number;
  readonly waitTime: Duration;
  readonly status: MergedPairStatus;
  readonly collisions: PairEntity[];
}
