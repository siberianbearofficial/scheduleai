import { TuiRoot } from "@taiga-ui/core";
import {Component, DestroyRef, inject, OnInit} from '@angular/core';
import {ChangeDetectionStrategy} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {UniversitiesService} from './services/universities.service';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';

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

  ngOnInit() {
    this.universitiesService.loadUniversities().pipe(
      takeUntilDestroyed(this.destroyRef),
    ).subscribe()
  }
}
