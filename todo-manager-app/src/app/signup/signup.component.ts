import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  @ViewChild('form') ngForm: NgForm;

  user: {
    userName: string;
    firstName: string;
    lastName: string;
    password: String;
    email: String;
  };

  login(): void {
    this.user = {
      userName: this.ngForm.form.value['userName'],
      firstName: this.ngForm.form.value['firstName'],
      lastName: this.ngForm.form.value['lastName'],
      password: this.ngForm.form.value['password'],
      email: this.ngForm.form.value['email'],
    };

    console.log(this.user);
  }
}
