import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  @ViewChild('form')
  loginForm: NgForm;

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService
      .login(this.loginForm.value.userName, this.loginForm.value.password)
      .subscribe({
        next: (response) => {
          this.router.navigate(['/todos']);
        },
        error: (error) => {
          console.log(error);
        },
      });

    this.loginForm.reset();
  }
}
