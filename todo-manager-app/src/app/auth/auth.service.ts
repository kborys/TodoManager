import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, tap } from 'rxjs';
import { User } from '../models/user.model';

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
  baseUrl = 'https://localhost:7110/api/users/';
  user = new BehaviorSubject<User>(null);
  private tokenExpirationTimer: any;

  constructor(private http: HttpClient, private router: Router) {}

  signup() {}

  login(userName: string, password: string) {
    const url = this.baseUrl + 'authenticate';
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    // TODO: error handling
    return this.http
      .post<AuthResponseData>(
        url,
        {
          userName: userName,
          password: password,
        },
        { headers: headers }
      )
      .pipe(
        tap((responseData) => {
          this.handleAuthentication(responseData);
        })
      );
  }

  private handleAuthentication(authResponse: AuthResponseData) {
    // TODO: token expiration date set to 1 hour. update the api to return "expiresIn" value
    const expiresIn = 60 * 60;
    const expirationDate = new Date(new Date().getTime() + 1000 * expiresIn);
    const user = new User(
      authResponse.user.userId,
      authResponse.user.userName,
      authResponse.user.firstName,
      authResponse.user.lastName,
      authResponse.user.emailAddress,
      authResponse.token,
      expirationDate
    );
    this.user.next(user);
    this.autoLogout(expiresIn * 1000);
    localStorage.setItem('userData', JSON.stringify(user));
  }

  logout() {
    this.user.next(null);
    this.router.navigate(['/login']);
    localStorage.removeItem('userData');
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
    } = JSON.parse(localStorage.getItem('userData'));
    if (!userData) {
      return;
    }

    const user = new User(
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
