import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Group } from 'src/shared/models/group.model';
import { GroupsService } from './groups.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css'],
})
export class GroupsComponent implements OnInit, OnDestroy {
  groups: Group[] = [];
  sub: Subscription;

  constructor(
    private groupsService: GroupsService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const groupId: number = +localStorage.getItem('lastGroupId');
    this.sub = this.groupsService.getGroups().subscribe((response) => {
      this.groups = response;
      if (groupId) {
        this.router.navigate([groupId], { relativeTo: this.route });
      } else {
        console.log(this.groups[0].groupId);

        this.router.navigate([this.groups[0].groupId], {
          relativeTo: this.route,
        });
      }
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
