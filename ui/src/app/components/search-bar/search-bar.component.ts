import { ChangeDetectionStrategy, Component, EventEmitter, forwardRef, Output } from '@angular/core';
import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { TuiTextfield } from '@taiga-ui/core';

@Component({
  standalone: true,
  exportAs: "search-bar",
  selector: "app-search-bar",
  imports: [FormsModule, ReactiveFormsModule, TuiTextfield],
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SimpleSearchBarComponent),
      multi: true
    }
  ]
})
export default class SimpleSearchBarComponent {
  value = '';
  disabled = false;
  @Output() enterPressed = new EventEmitter<void>();

  onChange: (value: string) => void = () => {};
  onTouched: () => void = () => {};

  writeValue(value: string): void {
    this.value = value;
  }

  registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  onInput(value: string): void {
    this.value = value;
    this.onChange(value);
    this.onTouched();
  }

  onKeydown(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this.enterPressed.emit();
    }
  }
}
