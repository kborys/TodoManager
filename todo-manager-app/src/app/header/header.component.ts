import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  groupsSub: Subscription;
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
        this.groupsService.getGroups();
        this.groupsSub = this.groupsService.groupsChanged.subscribe(
          (groups: Group[]) => {
            this.groups = groups;
            this.router.navigate([
              '/group',
              groupId ? groupId : this.groups[0].groupId,
            ]);
          }
        );
      }
    });
  }

  onLogout() {
    this.authService.logout();
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
    this.groupsSub.unsubscribe();
  }
}
