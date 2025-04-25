import {AsyncPipe} from '@angular/common';
import {ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit} from '@angular/core';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {TuiLet} from '@taiga-ui/cdk';
import {TuiDataList} from '@taiga-ui/core';
import {TuiDataListWrapperComponent, TuiFilterByInputPipe, TuiStringifyContentPipe} from '@taiga-ui/kit';
import {TuiComboBoxModule, TuiTextfieldControllerModule} from '@taiga-ui/legacy';

import {GroupsService} from '../../services/groups.service';
import {BehaviorSubject, combineLatest, distinctUntilChanged, map, Observable, tap} from 'rxjs';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {GroupEntity} from '../../entities/group-entity';
import {UniversityEntity} from '../../entities/university-entity';

@Component({
  standalone: true,
  exportAs: "app-group-selector",
  selector: 'app-group-selector',
  imports: [
    AsyncPipe,
    ReactiveFormsModule,
    TuiComboBoxModule,
    TuiDataList,
    TuiLet,
    TuiTextfieldControllerModule,
    TuiDataListWrapperComponent,
    TuiFilterByInputPipe,
    TuiStringifyContentPipe,
  ],
  templateUrl: './group-selector.component.html',
  styleUrls: ['./group-selector.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class GroupSelectorComponent implements OnInit {
  private readonly service = inject(GroupsService);
  private readonly destroyRef: DestroyRef = inject(DestroyRef);

  protected search$ = new BehaviorSubject<string | null>(null);

  protected readonly filtered$: Observable<GroupEntity[]> = combineLatest([this.search$, this.service.groups$]).pipe(
    map(([search, universities]) => universities
      .filter(e => e.name.includes(search ?? ""))
      .slice(0, 100)
    )
  )

  protected readonly control = new FormControl();

  ngOnInit() {
    this.service.selectedGroup$.pipe(
      distinctUntilChanged(),
      tap(u => this.control.setValue(u)),
      takeUntilDestroyed(this.destroyRef),
    ).subscribe();
    this.control.valueChanges.pipe(
      distinctUntilChanged(),
      tap(u => this.service.selectGroup(u)),
      takeUntilDestroyed(this.destroyRef)
    ).subscribe();
  }

  protected readonly stringify = (item: UniversityEntity): string => item.name;
}
