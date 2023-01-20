import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { TodoItem } from 'src/shared/models/todo-item.model';

@Injectable({ providedIn: 'root' })
export class TodosService {
  baseUrl = 'https://localhost:7110/api/groups';
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });

  constructor(private http: HttpClient) {}

  getTodos(groupId: number) {
    const url = this.baseUrl + '/' + groupId + '/todos';
    return this.http.get<TodoItem[]>(url, { headers: this.headers }).pipe(
      map((todos) => {
        return todos;
      })
    );
  }
}
