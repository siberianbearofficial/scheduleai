import {Component, Input} from '@angular/core';
import {TuiBadge, TuiStatus} from '@taiga-ui/kit'
import {MergedPairEntity} from '../../entities/merged-pairs-entity';

@Component({
  selector: 'app-pair',
  imports: [
    TuiBadge,
    TuiStatus
  ],
  templateUrl: './pair.component.html',
  styleUrl: './pair.component.scss'
})
export class PairComponent {
  @Input() pair!: MergedPairEntity;

}
