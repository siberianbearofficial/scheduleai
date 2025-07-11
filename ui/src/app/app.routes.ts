import { Routes } from '@angular/router';
import {MainPageComponent} from './pages/main-page/main-page.component';
import {MergedPairsPageComponent} from './pages/merged-pairs-page/merged-pairs-page.component';
import {ChatPageComponent} from './pages/chat-page/chat-page.component';

export const routes: Routes = [
  { path: '', component: MainPageComponent },
  { path: 'teacherSchedule/:teacherId', component: MergedPairsPageComponent},
  { path: 'chat', component: ChatPageComponent },
  { path: '**', redirectTo: '/', pathMatch: 'full' },
];
