import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {TuiSheetDialog, TuiSheetDialogOptions} from '@taiga-ui/addon-mobile';
import {TuiButton} from '@taiga-ui/core';

@Component({
  standalone: true,
  selector: 'app-footer',
  exportAs: "app-footer",
  imports: [TuiButton, TuiSheetDialog],
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class FooterComponent {
  protected open = false;

  protected readonly options: Partial<TuiSheetDialogOptions> = {
    label: 'Alexander Inkin',
    closeable: true,
  };
}
