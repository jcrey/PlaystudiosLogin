import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { BaseFormComponent } from '../base-form.component';
import { AuthService } from './auth.service';
import { LoginRequest } from './login-request';
import { LoginResult } from './login-result';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent extends BaseFormComponent implements OnInit {

  title?: string;
  loginResult?: LoginResult;
  pwdPattern = "^[a-zA-Z0-9@#$%&*()-_+]{8,15}$";

  constructor(private router: Router, private authService: AuthService) {
    super();
  }

  ngOnInit() {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.pattern(this.pwdPattern)])
    });
  }

  onSubmit() {
    var loginRequest = <LoginRequest>{};
    loginRequest.email = this.form.controls['email'].value;
    loginRequest.password = this.form.controls['password'].value;

    this.authService
      .login(loginRequest)
      .subscribe(result => {

        this.loginResult = result;
        if (result.success) {
          this.router.navigate(["/settings"]);
        }
      }, error => {
        console.log(error);
          this.loginResult = error.error;
      });
  }
}
