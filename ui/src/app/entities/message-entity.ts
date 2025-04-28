import {Moment} from 'moment';
import {PairEntity} from './pair-entity';

export enum MessageRole {
  User,
  Assistant,
}

export interface MessageEntity {
  readonly text: string;
  readonly role: MessageRole;
  readonly timestamp: Moment;
  readonly pairs: PairEntity[];
}
