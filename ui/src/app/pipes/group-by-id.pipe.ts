import {inject, Pipe, PipeTransform} from '@angular/core';
import {Observable, of} from 'rxjs';
import {GroupsService} from '../services/groups.service';
import {GroupEntity} from '../entities/group-entity';

@Pipe({
  standalone: true,
  name: 'groupById'
})
export class GroupByIdPipe implements PipeTransform {
  private readonly groupsService = inject(GroupsService);

  transform(value: string | undefined, ...args: unknown[]): Observable<GroupEntity | undefined> {
    if (!value)
      return of(undefined);
    return this.groupsService.groupById(value);
  }

}
