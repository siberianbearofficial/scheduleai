import {Duration, Moment} from 'moment';

export enum MergedPairStatus {
  BeforePairs,
  AfterPairs,
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

export const ComparePairsByStartTime = (a: PairEntity, b: PairEntity): number => a.startTime.diff(b.startTime)
export const ComparePairsByConvenience = (a: PairEntity, b: PairEntity): number =>
  (b.convenience?.coefficient ?? 0) - (a.convenience?.coefficient ?? 0)

