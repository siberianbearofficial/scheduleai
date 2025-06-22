import { Component } from '@angular/core';
import SearchBarComponent from '../../components/ai-search-bar/ai-search-bar.component';
import GroupSelectorComponent from '../../components/group-selector/group-selector.component';
import UniversitySelectorComponent from '../../components/university-selector/university-selector.component';
import {HeaderComponent} from '../../components/header/header.component';
import FooterComponent from '../../components/footer/footer.component';
import {TuiChip} from '@taiga-ui/kit';

@Component({
  selector: 'app-main-page',
  imports: [
    SearchBarComponent,
    GroupSelectorComponent,
    UniversitySelectorComponent,
    HeaderComponent,
    FooterComponent,
    SearchBarComponent,
    TuiChip
  ],
  templateUrl: './main-page.component.html',
  standalone: true,
  styleUrl: './main-page.component.scss'
})

export class MainPageComponent {
}
