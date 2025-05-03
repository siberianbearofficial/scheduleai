import {Moment} from 'moment';
import {PairEntity} from './pair-entity';

export enum MessageRole {
  User,
  Assistant,
}

export interface MessageEntity {
  readonly html: string;
  readonly role: MessageRole;
  readonly timestamp: Moment;
  readonly pairs: PairEntity[];
}
