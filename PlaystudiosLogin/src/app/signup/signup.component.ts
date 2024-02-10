import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { BaseFormComponent } from '../base-form.component';
import { SignupRequest } from './signup-request';
import { SignupResult } from './signup-result';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent extends BaseFormComponent implements OnInit {

  title?: string;
  signupResult?: SignupResult;
  pwdPattern = "^[a-zA-Z0-9@#$%&*()-_+]{8,15}$";

  constructor(private router: Router, private authService: AuthService) {
    super();
    this.title = 'Signup'
  }

  ngOnInit() {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.pattern(this.pwdPattern)])
    });
  }

  onSubmit() {
    var request = <SignupRequest>{};
    request.name = this.form.controls['name'].value;
    request.email = this.form.controls['email'].value;
    request.password = this.form.controls['password'].value;

    this.authService
      .signup(request)
      .subscribe(result => {
        this.signupResult = result;
        if (result.success) {
          this.router.navigate(["/login"]);
        }
      }, error => {
        console.log(error);
        if (error.status == 400) {
          this.signupResult = error.error;
        }
      });
  }
}

