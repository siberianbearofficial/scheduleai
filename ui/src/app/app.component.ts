import {TuiRoot} from "@taiga-ui/core";
import {Component, DestroyRef, inject, OnInit} from '@angular/core';
import {ChangeDetectionStrategy} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {UniversitiesService} from './services/universities.service';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {merge, Observable} from 'rxjs';
import {GroupsService} from './services/groups.service';
import {TeacherService} from './services/teachers.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TuiRoot],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements OnInit {
  private readonly destroyRef: DestroyRef = inject(DestroyRef);
  private readonly universitiesService: UniversitiesService = inject(UniversitiesService);
  private readonly groupsService: GroupsService = inject(GroupsService);
  private readonly teacherService: TeacherService = inject(TeacherService);

  ngOnInit() {
    this.mainObservables().pipe(
      takeUntilDestroyed(this.destroyRef),
    ).subscribe()
  }

  private mainObservables(): Observable<undefined> {
    return merge(
      this.universitiesService.loadUniversities(),
      this.universitiesService.saveSelectedUniversity$,
      this.groupsService.loadGroupsOnUniversityChange$,
      this.teacherService.loadTeachersOnUniversityChange$,
    )
  }
}
