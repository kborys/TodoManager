import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { TodoItem } from 'src/app/shared/models/todo-item.model';
import { TodoItemCreate } from '../shared/models/todo-item-create';

@Injectable({ providedIn: 'root' })
export class TodosService {
  baseUrl = 'https://localhost:7110/api';
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });
  private todos: TodoItem[] = [];
  todosChanged = new Subject<TodoItem[]>();

  constructor(private http: HttpClient) {}

  getTodos(groupId: number) {
    const url = this.baseUrl + '/groups/' + groupId + '/todos';
    this.http
      .get<TodoItem[]>(url, { headers: this.headers })
      .subscribe((todos: TodoItem[]) => {
        this.todos = todos;
        this.todosChanged.next(this.todos.slice());
      });
  }

  createTodo(request: TodoItemCreate) {
    const url = this.baseUrl + '/todos';
    this.http
      .post<TodoItem>(url, JSON.stringify(request), { headers: this.headers })
      .subscribe((todo: TodoItem) => {
        this.todos.push(todo);
        this.todosChanged.next(this.todos.slice());
      });
  }
}
