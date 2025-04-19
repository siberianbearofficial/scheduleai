import { Component } from '@angular/core';
import {LogoComponent} from '../logo/logo.component';
import {RouterLink} from "@angular/router";

@Component({
  exportAs: 'app-header',
  selector: 'app-header',
  imports: [
    LogoComponent,
    RouterLink
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

}
