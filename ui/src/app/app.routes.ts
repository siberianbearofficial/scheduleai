import { Routes } from '@angular/router';
import {MainPageComponent} from './pages/main-page/main-page.component';
import Example from './components/ai-search-bar/ai-search-bar.component';

export const routes: Routes = [
  { path: '', component: MainPageComponent },
  { path: 'main', component: MainPageComponent },
  { path: 'ai-search', component: Example},
  { path: '**',  redirectTo: '', pathMatch: 'full' },
];
