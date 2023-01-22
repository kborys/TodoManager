import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { Group } from '../../shared/models/group.model';

@Injectable({ providedIn: 'root' })
export class GroupsService {
  baseUrl = 'https://localhost:7110/api/groups';
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });

  constructor(private http: HttpClient) {}

  getGroup(groupId: number) {
    const url = this.baseUrl + '/' + groupId;
    return this.http.get<Group>(url, { headers: this.headers }).pipe(
      map((group) => {
        return group;
      })
    );
  }

  getGroups() {
    const url = this.baseUrl;
    return this.http.get<Group[]>(url, { headers: this.headers }).pipe(
      map((groups) => {
        return groups;
      })
    );
  }
}
