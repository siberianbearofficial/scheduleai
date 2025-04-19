import { Routes } from '@angular/router';
import {MainPageComponent} from './pages/main-page/main-page.component';
import {MergedPairsPageComponent} from './pages/merged-pairs-page/merged-pairs-page.component';

export const routes: Routes = [
  { path: '', component: MainPageComponent },
  { path: 'teacherSchedule', component: MergedPairsPageComponent},
  { path: '**', redirectTo: '/', pathMatch: 'full' },
];
