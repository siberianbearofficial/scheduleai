import {ChangeDetectionStrategy, Component} from '@angular/core';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-logo',
  exportAs: 'app-logo',
  imports: [
    RouterLink
  ],
  templateUrl: './logo.component.html',
  standalone: true,
  styleUrl: './logo.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LogoComponent {

}
