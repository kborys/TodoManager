import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../models/user.model';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  @ViewChild('form') ngForm: NgForm;
  // user: User = new User();

  constructor() {}

  signUp(): void {
    // this.user = this.ngForm.form.value['userData'];
    console.log('signup placeholder');
  }
}
