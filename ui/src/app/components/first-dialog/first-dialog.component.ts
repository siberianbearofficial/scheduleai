import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import UniversitySelectorComponent from '../university-selector/university-selector.component';
import {TuiButton, TuiDialogContext, TuiExpandComponent} from '@taiga-ui/core';
import GroupSelectorComponent from '../group-selector/group-selector.component';
import {UniversitiesService} from '../../services/universities.service';
import {map} from 'rxjs';
import {AsyncPipe} from '@angular/common';
import {injectContext} from '@taiga-ui/polymorpheus';
import {GroupsService} from '../../services/groups.service';

@Component({
  standalone: true,
  selector: 'app-first-dialog',
  imports: [
    UniversitySelectorComponent,
    TuiExpandComponent,
    GroupSelectorComponent,
    TuiButton,
    AsyncPipe,
  ],
  templateUrl: './first-dialog.component.html',
  styleUrl: './first-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FirstDialogComponent {
  private readonly universitiesService = inject(UniversitiesService);
  private readonly groupsService = inject(GroupsService);

  readonly context = injectContext<TuiDialogContext>();

  protected readonly universitySelected$ = this.universitiesService.selectedUniversity$.pipe(
    map(university => university !== null),
  )
  protected readonly groupSelected$ = this.groupsService.selectedGroup$.pipe(
    map(group => group !== null),
  )

  close() {
    this.context.completeWith();
  }
}
