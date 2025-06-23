import {Moment} from 'moment';
import {PairEntity} from './pair-entity';

export enum MessageRole {
  User,
  Assistant,
}

export interface MessageEntity {
  readonly html: string | null;
  readonly role: MessageRole;
  readonly timestamp: Moment;
  readonly pairs: PairEntity[];
  readonly toolCalls: ToolCallEntity[];
  readonly inProgress: boolean;
}

export interface ToolCallEntity {
  readonly name: string;
  readonly description: string;
  readonly parameter: string;
  readonly result: string | null;
  readonly error: string | null;
  readonly isSuccess: boolean;
}
