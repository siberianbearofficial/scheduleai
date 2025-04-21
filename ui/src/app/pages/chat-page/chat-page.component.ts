import {ChangeDetectionStrategy, Component, inject, OnInit} from '@angular/core';
import {TuiMessage} from '@taiga-ui/kit';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import {HeaderComponent} from '../../components/header/header.component';
import {AsyncPipe, DatePipe} from '@angular/common';
import SimpleSearchBarComponent from '../../components/search-bar/search-bar.component'
import {FormsModule} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import moment, {Moment} from 'moment';
import {ChatService} from '../../services/chat.service';
import {take} from 'rxjs';
import {MessageRole} from '../../entities/message-entity';

interface Message {
  text: string;
  sender: 'user' | 'server';
  timestamp: Moment;
}

@Component({
  standalone: true,
  selector: 'app-chat-page',
  exportAs: 'app-chat-page',
  imports: [
    TuiMessage,
    FormsModule,
    LogoComponent,
    FooterComponent,
    HeaderComponent,
    DatePipe,
    SimpleSearchBarComponent,
    AsyncPipe
  ],
  templateUrl: './chat-page.component.html',
  styleUrl: './chat-page.component.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatPageComponent implements OnInit {
  private readonly chatService: ChatService = inject(ChatService);
  newMessage = '';
  private readonly route: ActivatedRoute = inject(ActivatedRoute);

  constructor() {}

  protected readonly messages$ = this.chatService.messages$;

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['message']) {
        this.chatService.sendMessage(params['message']).pipe(
          take(1),
        ).subscribe();
      }
    });
  }


  protected readonly MessageRole = MessageRole;
}

