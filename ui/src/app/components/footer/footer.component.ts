import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {TuiSheetDialogService} from '@taiga-ui/addon-mobile';
import {TuiButton} from '@taiga-ui/core';
import {Subject, switchMap} from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-footer',
  exportAs: "app-footer",
  imports: [TuiButton],
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class FooterComponent {
  protected readonly stream$ = new Subject<void>();

  constructor() {
    const service = inject(TuiSheetDialogService);

    this.stream$
      .pipe(
        switchMap(() => service.open('bombardiro crocodilo', {label: 'About'})),
        takeUntilDestroyed(),
      )
      .subscribe();
  }
}
