import {ChangeDetectionStrategy, Component, DestroyRef, EventEmitter, inject, OnInit, Output} from '@angular/core';
import {FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TuiDropdown, TuiIcon, TuiTextfield} from '@taiga-ui/core';
import {TuiTooltip} from '@taiga-ui/kit';
import {TeacherEntity} from '../../entities/teacher-entity';
import {TeacherService} from '../../services/teachers.service';
import {combineLatest, map, Observable, tap, merge} from 'rxjs';
import {AsyncPipe} from '@angular/common';
import {RouterLink} from '@angular/router';
import {TeacherCardComponent} from '../teacher-card/teacher-card.component';
import {TuiLet} from '@taiga-ui/cdk';

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
  private readonly destroyRef = inject(DestroyRef);

  @Output() search = new EventEmitter<string>();

  protected readonly control = new FormControl<string>("");

  protected dropdownOpened = false;

  protected readonly filtered$ = combineLatest(this.control.valueChanges, this.teacherService.teachers$).pipe(
    tap(([search]) => this.dropdownOpened = search !== null && search.length >= 2),
    map(([search, teachers]) => teachers.filter(e => e.fullName.includes(search ?? "")))
  );

  protected readonly control = new FormControl<string>("");

  protected dropdownOpened = false;

  protected filtered: TeacherEntity[] = []

  ngOnInit() {
    combineLatest([this.control.valueChanges, this.teacherService.teachers$]).pipe(
      tap(([search]) => this.dropdownOpened = search !== null && search.length >= 2),
      tap(([search, teachers]) =>
        this.filtered = teachers.filter(e =>
          e.fullName.toLowerCase().includes(search?.toLowerCase() ?? "")).slice(0, 6)),
      takeUntilDestroyed(this.destroyRef),
    ).subscribe();
  }

  onSearch() {
    const query = this.control.value;
    if (query?.trim()) {
      this.search.emit(query);
    }
  }

  protected selectTeacher(teacher: TeacherEntity) {
    this.teacherService.selectTeacher(teacher);
  }
}
