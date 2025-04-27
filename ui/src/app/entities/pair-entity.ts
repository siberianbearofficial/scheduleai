import {Duration, Moment} from 'moment';

export enum MergedPairStatus {
  BeforePairs ,
  AfterPairs ,
  InGap,
  Collision,
  This,
  NoPairs
}

export interface PairEntity {
  readonly groups: string[];
  readonly teachers: string[];
  readonly startTime: Moment;
  readonly endTime: Moment;
  readonly actType: string;
  readonly discipline: string;
  readonly rooms: string[];
  readonly convenience: {
    readonly coefficient: number;
    readonly waitTime: Duration;
    readonly status: MergedPairStatus;
    readonly collisions: PairEntity[];
  } | null;
}
