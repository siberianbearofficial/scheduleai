import {inject, Pipe, PipeTransform} from '@angular/core';
import {Observable} from 'rxjs';
import {TeacherEntity} from '../entities/teacher-entity';
import {TeacherService} from '../services/teachers.service';

@Pipe({
  standalone: true,
  name: 'teacherById'
})
export class TeacherByIdPipe implements PipeTransform {
  private readonly teacherService = inject(TeacherService);

  transform(value: string, ...args: unknown[]): Observable<TeacherEntity | undefined> {
    return this.teacherService.teacherById(value);
  }

}
