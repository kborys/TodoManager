import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, tap } from 'rxjs';
import { AuthUser } from '../shared/models/auth-user.model';
import { Buffer } from 'buffer';
import { UserCreate } from '../shared/models/user-create.model';

interface AuthResponseData {
  user: {
    userId: number;
    userName: string;
    firstName: string;
    lastName: string;
    emailAddress: string;
  };
  token: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  user = new BehaviorSubject<AuthUser>(null);
  private tokenExpirationTimer: any;
  baseUrl = 'https://localhost:7110/api/users/';
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });

  // TODO: "remember me" checkbox -  jwt refresh token?

  constructor(private http: HttpClient, private router: Router) {}

  signup(user: UserCreate) {
    const url = this.baseUrl + 'register';

    return this.http.post<AuthUser>(
      url,
      {
        userName: user.userName,
        firstName: user.firstName,
        lastName: user.lastName,
        password: user.password,
        emailAddress: user.emailAddress,
      },
      { headers: this.headers }
    );
  }

  login(userName: string, password: string) {
    const url = this.baseUrl + 'authenticate';

    return this.http
      .post<AuthResponseData>(
        url,
        {
          userName: userName,
          password: password,
        },
        { headers: this.headers }
      )
      .pipe(
        tap((response) => {
          this.handleAuthentication(response);
        })
      );
  }

  private handleAuthentication(authResponse: AuthResponseData) {
    const expirationDate = this.getTokenExpirationDate(authResponse.token);
    const user = new AuthUser(
      authResponse.user.userId,
      authResponse.user.userName,
      authResponse.user.firstName,
      authResponse.user.lastName,
      authResponse.user.emailAddress,
      authResponse.token,
      expirationDate
    );
    this.user.next(user);
    const expiresIn = expirationDate.getTime() - Date.now();
    this.autoLogout(expiresIn);
    localStorage.setItem('user', JSON.stringify(user));
    this.router.navigate(['/group']);
  }

  private getTokenExpirationDate(token): Date {
    const payloadBase64 = token.split('.')[1];
    const decodedJson = Buffer.from(payloadBase64, 'base64').toString();
    const decoded = JSON.parse(decodedJson);
    return new Date(decoded.exp * 1000);
  }

  logout() {
    this.user.next(null);
    this.router.navigate(['/login']);
    localStorage.removeItem('user');
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
    }
    this.tokenExpirationTimer = null;
  }

  autoLogin() {
    const userData: {
      userId: string;
      userName: string;
      firstName: string;
      lastName: string;
      emailAddress: string;
      _token: string;
      _tokenExpirationDate: string;
    } = JSON.parse(localStorage.getItem('user'));
    if (!userData) return;

    const user = new AuthUser(
      +userData.userId,
      userData.userName,
      userData.firstName,
      userData.lastName,
      userData.emailAddress,
      userData._token,
      new Date(userData._tokenExpirationDate)
    );

    if (user.token) {
      this.user.next(user);
      const expirationDuration =
        new Date(userData._tokenExpirationDate).getTime() -
        new Date().getTime();
      this.autoLogout(expirationDuration);
    }
  }

  private autoLogout(expirationDuration: number) {
    this.tokenExpirationTimer = setTimeout(() => {
      this.logout();
    }, expirationDuration);
  }
}
