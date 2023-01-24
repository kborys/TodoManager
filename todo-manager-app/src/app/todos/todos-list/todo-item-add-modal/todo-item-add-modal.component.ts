import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/auth/auth.service';
import { Status } from 'src/app/shared/enums/status';
import { TodoItemCreate } from 'src/app/shared/models/todo-item-create';
import { TodosService } from '../../todos.service';

@Component({
  selector: 'app-todo-item-add-modal',
  templateUrl: './todo-item-add-modal.component.html',
  styleUrls: ['./todo-item-add-modal.component.css'],
})
export class TodoItemAddModalComponent implements OnInit {
  @Input() status: Status;
  @Input() groupId: number;
  @ViewChild('f')
  form: NgForm;

  constructor(
    public activeModal: NgbActiveModal,
    private authService: AuthService,
    private todosService: TodosService
  ) {}

  ngOnInit(): void {}

  onSubmit() {
    const title = this.form.value.title;
    const description = this.form.value.description;
    const activeUser = this.authService.user.value;
    const todo: TodoItemCreate = new TodoItemCreate(
      title,
      description,
      +this.groupId,
      activeUser.userId,
      +Status[this.status]
    );
    this.todosService.createTodo(todo);

    this.activeModal.close();
  }
}
