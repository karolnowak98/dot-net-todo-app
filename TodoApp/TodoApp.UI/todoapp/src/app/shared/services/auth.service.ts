import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

import { UserLoginDto } from "../interfaces/dtos/user-login-dto.interface";
import { ServiceResponse } from "../interfaces/service-response.interface";
import { environment } from "../utils/environment";
import { UserRegisterDto } from "../interfaces/dtos/user-register-dto.interface";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiEndpoints = {
    login: 'auth/login',
    register: 'auth/register'
  };

  private readonly tokenKey = 'jwtToken';

  constructor(private http: HttpClient, private router: Router) {}

  login(values: UserLoginDto): Observable<ServiceResponse<any>> {
    return this.http.post<ServiceResponse<any>>(environment.httpsUrl + this.apiEndpoints.login, values)
      .pipe(
        tap(response => {
          if (response.success && response.data) {
            localStorage.setItem(this.tokenKey, response.data);
          }
        })
      );
  }

  register(values: UserRegisterDto): Observable<ServiceResponse<any>> {
    return this.http.post<ServiceResponse<any>>(environment.httpsUrl + this.apiEndpoints.register, values);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.router.navigateByUrl('/');
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    return token !== null;
  }
}
