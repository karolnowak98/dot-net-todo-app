import { Component } from '@angular/core';
import {AuthService} from "../auth.service";
import {Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  email: string = '';
  password: string = '';
  showPassword: boolean = false;

  constructor(private authService: AuthService, private router: Router) { }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  loginForm = new FormGroup({
    email : new FormControl('',Validators.required),
    password: new FormControl('',Validators.required)
  })

  onLogin() {
    this.authService.login(this.loginForm.value).subscribe({
      next: () => this.router.navigateByUrl('/home'),
      error: () => alert("Please check credential...!")
    })
  }
}
