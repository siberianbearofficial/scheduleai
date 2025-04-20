import {ChangeDetectionStrategy, Component, EventEmitter, Output} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {TuiIcon, TuiTextfield} from '@taiga-ui/core';
import {TuiTooltip} from '@taiga-ui/kit';

@Component({
  standalone: true,
  exportAs: "search-bar",
  selector: "app-ai-search-bar",
  imports: [FormsModule, TuiIcon, TuiTextfield, TuiTooltip],
  templateUrl: './ai-search-bar.component.html',
  styleUrls: ['./ai-search-bar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class SearchBarComponent {
  searchQuery = '';
  @Output() search = new EventEmitter<string>();

  onSearch() {
    if (this.searchQuery.trim()) {
      this.search.emit(this.searchQuery);
    }
  }
}
