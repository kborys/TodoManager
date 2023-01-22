import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Group } from 'src/shared/models/group.model';
import { GroupsService } from './groups.service';

@Component({
  selector: 'app-groups',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css'],
})
export class GroupComponent implements OnInit, OnDestroy {
  group: Group;
  groupSub: Subscription;

  constructor(
    private groupsService: GroupsService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    let actualGroupId = this.route.snapshot.params['id'];
    console.log(actualGroupId);

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

/*
  todos: TodoItem[] = [];
  todosSub: Subscription;

  constructor(
    private todosService: TodosService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const groupId = params['id'];
      this.todosSub = this.todosService
        .getTodos(groupId)
        .subscribe((response) => {
          this.todos = response;
        });
      localStorage.setItem('lastGroupId', groupId);
    });
  }

  ngOnDestroy(): void {
    this.todosSub.unsubscribe();
  }

*/
