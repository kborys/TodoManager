import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';
import { TodosService } from 'src/app/todos/todos.service';
import { Status } from 'src/app/shared/enums/status';
import { TodoItem } from 'src/app/shared/models/todo-item.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TodoItemAddModalComponent } from './todo-item-add-modal/todo-item-add-modal.component';

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
  groupId: number;

  constructor(
    private todosService: TodosService,
    private route: ActivatedRoute,
    private modal: NgbModal
  ) {
    Object.keys(Status).forEach((item) => {
      if (isNaN(Number(item)) && item != 'Deleted')
        this.viewStatuses.push(item);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.groupId = params['id'];
      this.todosService.getTodos(this.groupId);
      this.todosSub = this.todosService.todosChanged.subscribe(
        (todos) => (this.todos = todos)
      );
    });
  }

  onAddTodo(status: Status) {
    const modalRef = this.modal.open(TodoItemAddModalComponent);
    modalRef.componentInstance.status = status;
    modalRef.componentInstance.groupId = this.groupId;
  }

  ngOnDestroy(): void {
    this.todosSub.unsubscribe();
  }
}
