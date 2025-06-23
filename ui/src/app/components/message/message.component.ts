import {ChangeDetectionStrategy, Component, Input} from '@angular/core';
import {MessageEntity, MessageRole} from '../../entities/message-entity';
import {TuiAccordion, TuiMessage} from '@taiga-ui/kit';
import {TuiIcon, TuiLoader} from '@taiga-ui/core';
import {PairComponent} from '../pair/pair.component'
import {NgDompurifyModule} from '@tinkoff/ng-dompurify';
import {ToolCallComponent} from '../tool-call/tool-call.component';

@Component({
  selector: 'app-message',
  imports: [
    TuiMessage,
    TuiIcon,
    PairComponent,
    NgDompurifyModule,
    TuiLoader,
    ToolCallComponent,
    TuiAccordion
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
