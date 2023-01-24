import { Status } from '../enums/status';

export class TodoItemCreate {
  constructor(
    public title: string,
    public description: string,
    public groupId: number,
    public ownerId: number,
    public status: number
  ) {}
}
