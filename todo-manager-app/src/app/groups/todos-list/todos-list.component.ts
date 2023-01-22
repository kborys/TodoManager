import { Component } from '@angular/core';
import { Status } from 'src/shared/enums/status';

@Component({
  selector: 'app-todos-list',
  templateUrl: './todos-list.component.html',
  styleUrls: ['./todos-list.component.css'],
  host: {
    class: 'flex-grow-1 d-flex flex-column',
  },
})
export class TodosListComponent {
  statuses = [];

  constructor() {
    Object.keys(Status).forEach((item) => {
      if (isNaN(Number(item)) && item != 'Deleted') this.statuses.push(item);
    });
  }
}
