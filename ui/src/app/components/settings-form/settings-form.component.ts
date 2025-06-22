import {ChangeDetectionStrategy, Component} from '@angular/core';
import {LogoComponent} from '../logo/logo.component';
import UniversitySelectorComponent from '../university-selector/university-selector.component';
import GroupSelectorComponent from '../group-selector/group-selector.component';
import {TuiButton} from '@taiga-ui/core';

@Component({
  standalone: true,
  selector: 'app-settings-form',
  imports: [
    LogoComponent,
    UniversitySelectorComponent,
    GroupSelectorComponent,
    TuiButton
  ],
  templateUrl: './settings-form.component.html',
  styleUrl: './settings-form.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SettingsFormComponent {

}
