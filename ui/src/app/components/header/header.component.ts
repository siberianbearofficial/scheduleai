import {ChangeDetectionStrategy, Component} from '@angular/core';
import {LogoComponent} from '../logo/logo.component';
import {RouterLink} from "@angular/router";
import UniversitySelectorComponent from '../university-selector/university-selector.component';
import GroupSelectorComponent from '../group-selector/group-selector.component';

@Component({
  exportAs: 'app-header',
  selector: 'app-header',
  imports: [
    LogoComponent,
    RouterLink,
    UniversitySelectorComponent,
    GroupSelectorComponent
  ],
  templateUrl: './header.component.html',
  standalone: true,
  styleUrl: './header.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {

}
