import {ChangeDetectionStrategy, Component} from '@angular/core';
import {LogoComponent} from '../logo/logo.component';
import {TuiButton, tuiDialog} from '@taiga-ui/core';
import {SettingsFormComponent} from '../settings-form/settings-form.component';

@Component({
  exportAs: 'app-header',
  selector: 'app-header',
  imports: [
    LogoComponent,
    TuiButton
  ],
  templateUrl: './header.component.html',
  standalone: true,
  styleUrl: './header.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
  private readonly dialog = tuiDialog(SettingsFormComponent, {
    dismissible: true,
  });

  openSettings() {
    this.dialog().subscribe();
  }
}
