export class AuthUser {
  constructor(
    public userId: number,
    public userName: string,
    public firstName: string,
    public lastName: string,
    public emailAddress: string,
    private _token: string,
    private _tokenExpirationDate: Date
  ) {}

  get token() {
    if (!this._tokenExpirationDate || new Date() > this._tokenExpirationDate)
      return null;
    return this._token;
  }
}
