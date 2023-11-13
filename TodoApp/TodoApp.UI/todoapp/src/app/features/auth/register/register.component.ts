import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from "@angular/router";
import { AuthService } from "../../../services/auth.service";
import { CustomValidators } from "../../../shared/utils/custom-validators";
import {UserRegisterDto} from "../../../shared/interfaces/dtos/user-register-dto.interface";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent {
  userRegisterDto : UserRegisterDto = { firstName: '', lastName:'', password:'', email:''};

  form: FormGroup;
  formSubmitted: boolean = false;
  showPassword: boolean = false;
  modalMessage = '';
  modalTitle = '';

  constructor(private authService: AuthService, private fb: FormBuilder, private router: Router) {
    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(10), CustomValidators.password()]]
    });

    if (this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/tasks');
    }
  }

  onRegister() {
    this.formSubmitted = true;

    if (!this.form.valid) return;

    this.userRegisterDto = { ...this.form.value };
    this.authService.register(this.userRegisterDto).pipe(
      catchError((errorResponse) => {
        this.modalTitle = "Error!";
        this.modalMessage = errorResponse.error.message;
        return of(null);
      })
    ).subscribe({
      next: (response) => {
        if (response && response.success) {
          this.modalTitle = "Success!";
          this.modalMessage = "Successfully created User";
        }
      }
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  redirectToLogin() {

    this.router.navigateByUrl('/')
  }

  tryAgain() {
    this.formSubmitted = false;
    this.form.reset();
  }
}
