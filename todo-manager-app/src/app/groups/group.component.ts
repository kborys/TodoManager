import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Group } from 'src/app/shared/models/group.model';
import { GroupsService } from './groups.service';

@Component({
  selector: 'app-groups',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css'],
  host: {
    class: 'flex-fill d-flex flex-column',
  },
})
export class GroupComponent implements OnInit, OnDestroy {
  group: Group = {} as Group;
  groupSub: Subscription;

  constructor(
    private groupsService: GroupsService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      const groupId = params['id'];
      this.groupSub = this.groupsService
        .getGroup(groupId)
        .subscribe((response) => {
          this.group = response;
        });
      localStorage.setItem('lastGroupId', groupId);
    });
  }

  ngOnDestroy(): void {
    this.groupSub.unsubscribe();
  }
}
