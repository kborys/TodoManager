import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Group } from '../shared/models/group.model';
import { User } from '../shared/models/user.model';

@Injectable({ providedIn: 'root' })
export class GroupsService {
  baseUrl = 'https://localhost:7110/api/groups';
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });
  private groups: Group[] = [];
  groupsChanged = new Subject<Group[]>();
  private members: User[] = [];
  membersChanged = new Subject<User[]>();

  constructor(private http: HttpClient) {}

  getGroup(groupId: number) {
    const url = this.baseUrl + '/' + groupId;

    return this.http.get<Group>(url, { headers: this.headers });
  }

  getGroups() {
    const url = this.baseUrl;
    this.http
      .get<Group[]>(url, { headers: this.headers })
      .subscribe((groups: Group[]) => {
        this.groups = groups;
        this.groupsChanged.next(this.groups.slice());
      });
  }

  getGroupMembers(groupId: number) {
    const url = this.baseUrl + '/' + groupId + '/members';
    this.http
      .get<User[]>(url, { headers: this.headers })
      .subscribe((members: User[]) => {
        this.members = members;
        this.membersChanged.next(this.members.slice());
      });
  }

  addGroupMember(userName: string, groupId: number) {
    const url = this.baseUrl + '/' + groupId + '/members';
    this.http
      .post(url, '{"userName": "' + userName + '"}', { headers: this.headers })
      .subscribe(() => this.getGroupMembers(groupId));
  }

  removeGroupMember(userId: number, groupId: number) {
    const url = this.baseUrl + '/' + groupId + '/members/' + userId;
    this.http
      .delete(url, { headers: this.headers })
      .subscribe(() => this.getGroupMembers(groupId));
  }
}
