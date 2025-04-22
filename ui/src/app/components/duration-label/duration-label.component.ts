import {ChangeDetectionStrategy, Component, Input} from '@angular/core';
import {Duration} from 'moment';

@Component({
  selector: 'app-duration-label',
  imports: [],
  templateUrl: './duration-label.component.html',
  standalone: true,
  styleUrl: './duration-label.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DurationLabelComponent {
  @Input() time: Duration | undefined = undefined;
}
