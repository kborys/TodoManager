import { Status } from '../enums/status';

export class TodoItem {
  constructor(
    public todoId: number,
    public title: string,
    public description: string,
    public groupId: number,
    public ownerId: number,
    public status: Status
  ) {}
}
