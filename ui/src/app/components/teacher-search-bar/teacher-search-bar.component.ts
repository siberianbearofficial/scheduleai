import {ChangeDetectionStrategy, Component} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {TuiTextfield} from '@taiga-ui/core';

@Component({
  standalone: true,
  exportAs: "app-teacher-search-bar",
  selector: "app-teacher-search-bar",
  imports: [FormsModule, TuiTextfield],
  templateUrl: './teacher-search-bar.component.html',
  styleUrls: ['./teacher-search-bar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class TeacherSearchBarComponent {
  protected value = '';
}
