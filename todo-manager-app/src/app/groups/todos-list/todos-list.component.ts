import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { TodosService } from 'src/app/todos/todos.service';
import { Status } from 'src/shared/enums/status';
import { TodoItem } from 'src/shared/models/todo-item.model';

@Component({
  selector: 'app-todos-list',
  templateUrl: './todos-list.component.html',
  styleUrls: ['./todos-list.component.css'],
})
export class TodosListComponent implements OnInit, OnDestroy {
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
}
