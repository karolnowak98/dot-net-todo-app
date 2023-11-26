import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { catchError } from "rxjs/operators";
import { of } from "rxjs";

import { UserLoginDto } from "../../../shared/interfaces/dtos/user-login-dto.interface";
import { AuthService } from "../../../shared/services/auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  userLoginDto: UserLoginDto = { email: '', password: '' };

  form: FormGroup;
  formSubmitted: boolean = false;
  showPassword: boolean = false;

  constructor(private authService: AuthService, private router: Router, private fb: FormBuilder) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });

    if (this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/tasks');
    }
  }

  onLogin() {
    this.formSubmitted = true;

    if (!this.form.valid) return;

    this.userLoginDto = { ...this.form.value };
    this.authService.login(this.userLoginDto).pipe(
      catchError((errorResponse) => {
        if(errorResponse.error instanceof ErrorEvent) {
          alert(errorResponse.error.message);
        } else {
          if(errorResponse.status === 0){
            alert("Api doesn't respond!");
          }
        }
        return of(null);
      })
    ).subscribe({
      next: (response) => {
        if (response && response.success) {
          this.router.navigateByUrl('/tasks');
        }
      }
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}
