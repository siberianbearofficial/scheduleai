import {ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit} from '@angular/core';
import {TuiMessage} from '@taiga-ui/kit';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import {HeaderComponent} from '../../components/header/header.component';
import {AsyncPipe, DatePipe} from '@angular/common';
import SimpleSearchBarComponent from '../../components/search-bar/search-bar.component'
import {FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {ChatService} from '../../services/chat.service';
import {first, take} from 'rxjs';
import {MessageRole} from '../../entities/message-entity';
import {TuiTextfieldComponent, TuiTextfieldDirective} from '@taiga-ui/core';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';

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
    AsyncPipe,
    TuiTextfieldComponent,
    TuiTextfieldDirective,
    ReactiveFormsModule
  ],
  templateUrl: './chat-page.component.html',
  styleUrl: './chat-page.component.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatPageComponent implements OnInit {
  private readonly chatService: ChatService = inject(ChatService);
  private readonly route: ActivatedRoute = inject(ActivatedRoute);
  private readonly destroyRef: DestroyRef = inject(DestroyRef);

  protected readonly messageInputControl = new FormControl('');

  protected readonly messages$ = this.chatService.messages$;

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['message']) {
        this.sendMessage(params['message']);
      }
    });
  }

  protected readonly MessageRole = MessageRole;

  private sendMessage(message: string): void {
    this.chatService.sendMessage(message).pipe(
      first(),
      takeUntilDestroyed(this.destroyRef),
    ).subscribe();
  }

  protected onSubmit() {
    if (this.messageInputControl.value)
      this.sendMessage(this.messageInputControl.value)
    this.messageInputControl.setValue("");
  }
}

