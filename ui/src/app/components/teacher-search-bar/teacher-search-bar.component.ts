import {ChangeDetectionStrategy, Component} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {TuiIcon, TuiTextfield} from '@taiga-ui/core';
import {TuiTooltip} from '@taiga-ui/kit';

@Component({
  standalone: true,
  exportAs: "app-teacher-search-bar",
  selector: "app-teacher-search-bar",
  imports: [FormsModule, TuiIcon, TuiTextfield, TuiTooltip],
  templateUrl: './teacher-search-bar.component.html',
  styleUrls: ['./teacher-search-bar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class TeacherSearchBarComponent {
  protected value = '';
}
