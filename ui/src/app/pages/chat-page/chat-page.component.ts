import {ChangeDetectionStrategy, Component} from '@angular/core';
import {TuiMessage} from '@taiga-ui/kit';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import {HeaderComponent} from '../../components/header/header.component';
import {DatePipe} from '@angular/common';
import SimpleSearchBarComponent from '../../components/search-bar/search-bar.component'
import {FormsModule} from '@angular/forms';
import {ActivatedRoute, NavigationStart, Router} from '@angular/router';
import {filter} from 'rxjs';

interface Message {
  text: string;
  sender: 'user' | 'server';
  timestamp: Date;
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
    SimpleSearchBarComponent
  ],
  templateUrl: './chat-page.component.html',
  styleUrl: './chat-page.component.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatPageComponent {
  messages: Message[] = [];
  newMessage = '';

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['message']) {
        const userMessage = params['message'];
        this.addMessage(userMessage, 'user');
        this.addMessage(this.getServerResponse(userMessage), 'server');
      }
    });
  }

  sendMessage() {
    if (!this.newMessage.trim()) return;

    // Добавляем сообщение пользователя
    this.addMessage(this.newMessage, 'user');
    const userMessage = this.newMessage;
    this.newMessage = '';
    const response = this.getServerResponse(userMessage);
    this.addMessage(response, 'server');
  }

  private addMessage(text: string, sender: 'user' | 'server') {
    this.messages.push({
      text,
      sender,
      timestamp: new Date()
    });
  }

  private getServerResponse(userMessage: string): string {
    const responses = [
      "Я получил ваше сообщение: " + userMessage,
      "Интересный вопрос!",
      "Давайте обсудим это подробнее.",
      "Спасибо за ваше сообщение!",
      "Я обрабатываю ваш запрос..."
    ];

    return responses[Math.floor(Math.random() * responses.length)];
  }
}

