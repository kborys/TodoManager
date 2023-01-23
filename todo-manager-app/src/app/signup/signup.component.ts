import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { UserCreateRequest } from '../shared/models/userCreateRequest.model';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  @ViewChild('form') ngForm: NgForm;
  isSignedUp = false;
  isDataValid = true;
  errorMessage = '';

  constructor(private authService: AuthService) {}

  signUp(): void {
    const user: UserCreateRequest = this.ngForm.form.value['user'];

    this.authService.signup(user).subscribe({
      next: (response) => {
        this.isDataValid = true;
        this.isSignedUp = true;
        this.ngForm.reset();
      },
      error: (error) => {
        this.errorMessage = error.error.message;
        this.ngForm.form.patchValue({ userData: { password: '' } });
        this.isDataValid = false;
      },
    });
  }
}
