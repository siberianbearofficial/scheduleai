import { Component } from '@angular/core';
import SearchBarComponent from '../../components/ai-search-bar/ai-search-bar.component';
import GroupSelectorComponent from '../../components/group-selector/group-selector.component';
import UniversitySelectorComponent from '../../components/university-selector/university-selector.component';
import {HeaderComponent} from '../../components/header/header.component';
import FooterComponent from '../../components/footer/footer.component';

@Component({
  selector: 'app-main-page',
  imports: [
    SearchBarComponent,
    GroupSelectorComponent,
    UniversitySelectorComponent,
    HeaderComponent,
    FooterComponent,
    SearchBarComponent
  ],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})

export class MainPageComponent {

}
