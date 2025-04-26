import {inject, Injectable} from '@angular/core';
import {WA_LOCAL_STORAGE} from '@ng-web-apis/common';
import {UniversityEntity} from '../entities/university-entity';
import {GroupEntity} from '../entities/group-entity';

const selectedUniversityKey = "scheduleai-selectedUniversity";
const selectedGroupKey = "scheduleai-selectedGroup";

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  private readonly localStorage = inject(WA_LOCAL_STORAGE);

  getUniversity(universities: UniversityEntity[]): UniversityEntity | null {
    const selectedUniversityId = this.localStorage.getItem(selectedUniversityKey);
    universities = universities?.filter(u => u.id === selectedUniversityId);
    if (universities == undefined || universities.length != 1) {
      return null;
    }
    return universities[0];
  }

  setUniversity(university: UniversityEntity | null): void {
    if (university === null) {
      this.localStorage.removeItem(selectedUniversityKey);
    } else {
      this.localStorage.setItem(selectedUniversityKey, university.id);
    }
  }

  getGroup(universities: GroupEntity[]): GroupEntity | null {
    const selectedGroupId = this.localStorage.getItem(selectedGroupKey);
    universities = universities?.filter(u => u.id === selectedGroupId);
    if (universities == undefined || universities.length != 1) {
      return null;
    }
    return universities[0];
  }

  setGroup(group: GroupEntity | null): void {
    if (group === null) {
      this.localStorage.removeItem(selectedGroupKey);
    } else {
      this.localStorage.setItem(selectedGroupKey, group.id);
    }
  }
}
