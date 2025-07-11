import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {tuiDialog, TuiDropdown, TuiIcon, TuiTextfield} from '@taiga-ui/core';
import {TuiTooltip} from '@taiga-ui/kit';
import {TeacherEntity} from '../../entities/teacher-entity';
import {TeacherService} from '../../services/teachers.service';
import {combineLatest, map, tap} from 'rxjs';
import {AsyncPipe} from '@angular/common';
import {Router, RouterLink} from '@angular/router';
import {TeacherCardComponent} from '../teacher-card/teacher-card.component';
import {TuiLet} from '@taiga-ui/cdk';
import {MobileSearchDialogComponent} from '../mobile-search-dialog/mobile-search-dialog.component';

@Component({
  standalone: true,
  exportAs: "search-bar",
  selector: "app-ai-search-bar",
  imports: [FormsModule, TuiIcon, TuiTextfield, TuiTooltip, TuiDropdown, ReactiveFormsModule, AsyncPipe, RouterLink, TeacherCardComponent, TuiLet],
  templateUrl: './ai-search-bar.component.html',
  styleUrls: ['./ai-search-bar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class SearchBarComponent {
  private readonly teacherService = inject(TeacherService);
  private readonly router = inject(Router);

  protected readonly control = new FormControl<string>("");

  protected dropdownOpened = false;

  protected readonly filtered$ = combineLatest(this.control.valueChanges, this.teacherService.teachers$).pipe(
    tap(([search]) => this.dropdownOpened = search !== null && search.length >= 2),
    map(([search, teachers]) => teachers.filter(e => e.fullName.includes(search ?? "")))
  );

  private readonly dialog = tuiDialog(MobileSearchDialogComponent, {
    size: 'page',
    closeable: false,
    dismissible: true,
  });

  onSearch() {
    const query = this.control.value;
    if (query?.trim()) {
      void this.router.navigate(['/chat'], {
        queryParams: {message: query},
      });
    }
  }

  onClick() {
    if (window.innerWidth < 767)
      this.dialog(undefined).subscribe();
  }

  protected selectTeacher(teacher: TeacherEntity) {
    this.teacherService.selectTeacher(teacher);
  }
}
