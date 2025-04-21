import {Moment} from 'moment';

export enum MessageRole {
  User,
  Assistant,
}

export interface MessageEntity {
  readonly content: string;
  readonly role: MessageRole;
  readonly timestamp: Moment;
}
