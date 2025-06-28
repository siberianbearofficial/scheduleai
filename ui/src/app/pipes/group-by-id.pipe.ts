import {inject, Pipe, PipeTransform} from '@angular/core';
import {Observable} from 'rxjs';
import {GroupsService} from '../services/groups.service';
import {GroupEntity} from '../entities/group-entity';

@Pipe({
  standalone: true,
  name: 'groupById'
})
export class GroupByIdPipe implements PipeTransform {
  private readonly groupsService = inject(GroupsService);

  transform(value: string, ...args: unknown[]): Observable<GroupEntity | undefined> {
    return this.groupsService.groupById(value);
  }

}
