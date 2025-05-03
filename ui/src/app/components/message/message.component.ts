import {ChangeDetectionStrategy, Component, Input} from '@angular/core';
import {MessageEntity, MessageRole} from '../../entities/message-entity';
import {TuiMessage} from '@taiga-ui/kit';
import {TuiIcon} from '@taiga-ui/core';
import {PairComponent} from '../pair/pair.component'
import {NgDompurifyModule} from '@tinkoff/ng-dompurify';

@Component({
  selector: 'app-message',
  imports: [
    TuiMessage,
    TuiIcon,
    PairComponent,
    NgDompurifyModule
  ],
  templateUrl: './message.component.html',
  standalone: true,
  styleUrl: './message.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MessageComponent {
  @Input() message!: MessageEntity;
  protected readonly MessageRole = MessageRole;
}
