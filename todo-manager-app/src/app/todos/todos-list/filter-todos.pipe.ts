import { Pipe, PipeTransform } from '@angular/core';
import { Status } from 'src/app/shared/enums/status';
import { TodoItem } from 'src/app/shared/models/todo-item.model';

@Pipe({
  name: 'filterTodos',
})
export class FilterTodosPipe implements PipeTransform {
  transform(todos: TodoItem[], status: string) {
    return todos.filter((todo) => {
      return Status[todo.status] === status;
    });
  }
}
