import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { TodosService } from 'src/app/todos/todos.service';
import { Status } from 'src/app/shared/enums/status';
import { TodoItem } from 'src/app/shared/models/todo-item.model';

@Component({
  selector: 'app-todos-list',
  templateUrl: './todos-list.component.html',
  styleUrls: ['./todos-list.component.css'],
  host: {
    class: 'flex-grow-1 d-flex flex-column',
  },
})
export class TodosListComponent implements OnInit, OnDestroy {
  viewStatuses: string[] = [];
  todos: TodoItem[] = [];
  todosSub: Subscription;

  constructor(
    private todosService: TodosService,
    private route: ActivatedRoute
  ) {
    Object.keys(Status).forEach((item) => {
      if (isNaN(Number(item)) && item != 'Deleted')
        this.viewStatuses.push(item);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const groupId = params['id'];
      this.todosSub = this.todosService
        .getTodos(groupId)
        .subscribe((response) => {
          this.todos = response;
          console.log(response);
        });
    });
  }

  ngOnDestroy(): void {
    this.todosSub.unsubscribe();
  }
}

/*
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
