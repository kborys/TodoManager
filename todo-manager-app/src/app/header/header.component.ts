import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbDropdownConfig } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Group } from 'src/app/shared/models/group.model';
import { AuthService } from '../auth/auth.service';
import { GroupsService } from '../groups/groups.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  userSub: Subscription;
  groupSub: Subscription;
  isAuthenticated = false;
  groups: Group[] = [];

  constructor(
    private authService: AuthService,
    private groupsService: GroupsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userSub = this.authService.user.subscribe((user) => {
      this.isAuthenticated = !user ? false : true;

      if (this.isAuthenticated) {
        const groupId: number = +localStorage.getItem('lastGroupId');
        this.groupSub = this.groupsService.getGroups().subscribe((response) => {
          this.groups = response;
          this.router.navigate([
            '/group',
            groupId ? groupId : this.groups[0].groupId,
          ]);
        });
      }
    });
  }

  onLogout() {
    this.authService.logout();
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
    this.groupSub.unsubscribe();
  }
}
