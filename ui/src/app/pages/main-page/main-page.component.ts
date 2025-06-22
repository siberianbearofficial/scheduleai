import {Component, DestroyRef, inject, OnInit} from '@angular/core';
import SearchBarComponent from '../../components/ai-search-bar/ai-search-bar.component';
import {TuiChip} from '@taiga-ui/kit';
import {UniversitiesService} from '../../services/universities.service';
import {tuiDialog} from '@taiga-ui/core';
import {FirstDialogComponent} from '../../components/first-dialog/first-dialog.component';
import {tap} from 'rxjs';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {LoadingStatus} from '../../entities/LoadingStatus';
import {HeaderComponent} from '../../components/header/header.component';

@Component({
  selector: 'app-main-page',
  imports: [
    SearchBarComponent,
    TuiChip,
    HeaderComponent
  ],
  templateUrl: './main-page.component.html',
  standalone: true,
  styleUrl: './main-page.component.scss'
})

export class MainPageComponent implements OnInit {
  private readonly destroyRef = inject(DestroyRef);
  private readonly universitiesService = inject(UniversitiesService);

  ngOnInit() {
    this.universitiesService.universitiesInfo$.pipe(
      tap(state => {
        if (state.loadingSelectedStatus === LoadingStatus.Completed && state.selectedUniversity === null)
          this.openDialog();
      }),
      takeUntilDestroyed(this.destroyRef),
    ).subscribe();
  }

  private readonly dialog = tuiDialog(FirstDialogComponent, {
    size: "m",
    dismissible: false,
    closeable: false,
  });

  openDialog() {
    this.dialog(undefined).subscribe();
  }
}
