export class UserCreateRequest {
  constructor(
    public userName: string,
    public firstName: string,
    public lastName: string,
    public password: string,
    public emailAddress: string
  ) {}
}
