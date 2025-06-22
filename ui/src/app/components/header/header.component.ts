import {ChangeDetectionStrategy, Component} from '@angular/core';
import {LogoComponent} from '../logo/logo.component';
import {RouterLink} from "@angular/router";
import UniversitySelectorComponent from '../university-selector/university-selector.component';
import GroupSelectorComponent from '../group-selector/group-selector.component';
import {TuiButton, tuiDialog} from '@taiga-ui/core';
import {SettingsFormComponent} from '../settings-form/settings-form.component';

@Component({
  exportAs: 'app-header',
  selector: 'app-header',
  imports: [
    LogoComponent,
    RouterLink,
    UniversitySelectorComponent,
    GroupSelectorComponent,
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
