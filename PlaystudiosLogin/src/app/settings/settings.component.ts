import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { environment } from '../../environments/environment';
import { BaseFormComponent } from '../base-form.component';
import { ResetPasswordRequest } from './ResetPasswordRequest';
import { ResetPasswordResult } from './ResetPasswordResult';
import { UserInfo } from './UserInfo';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent extends BaseFormComponent implements OnInit {

  userInfo?: UserInfo;
  resetPasswordResult?: ResetPasswordResult;
  pwdPattern = "^[a-zA-Z0-9@#$%&*()-_+]{8,15}$";

  constructor(protected http: HttpClient) {
      super();
  }

  ngOnInit(): void {
    var url = environment.baseUrl + "api/Account/Settings";

    this.http.get<UserInfo>(url)
      .subscribe(result => {
        console.log(result);
        this.userInfo = result;
      });

    this.form = new FormGroup({
      password: new FormControl('', [Validators.required, Validators.pattern(this.pwdPattern)])
    });
  }

  onSubmit() {
    var request = <ResetPasswordRequest>{};
    request.email = this.userInfo?.email;
    request.token = this.userInfo?.resetPasswordToken;
    request.password = this.form.controls['password'].value;

    var url = environment.baseUrl + "api/Account/ResetPassword";

    this.http.post<ResetPasswordResult>(url, request)
      .subscribe(result => {
        this.resetPasswordResult = result;
        if (result.success) {
          this.displayResetPasswordMessage();
        }
      });
  }

  displayResetPasswordMessage() {
    
  }
} 
