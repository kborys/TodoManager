import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
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
export class GroupComponent implements OnInit {
  group: Group = {} as Group;

  constructor(
    private groupsService: GroupsService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      const groupId = params['id'];
      this.groupsService.getGroup(groupId).subscribe((response) => {
        this.group = response;
      });
      localStorage.setItem('lastGroupId', groupId);
    });
  }
}
