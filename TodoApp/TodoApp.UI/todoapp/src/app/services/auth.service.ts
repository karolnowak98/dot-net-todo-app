import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import {Observable, tap} from 'rxjs';

import { ServiceResponse } from '../shared/interfaces/service-response.interface';
import { UserLoginDto } from '../shared/interfaces/dtos/user-login-dto.interface';
import { UserRegisterDto } from '../shared/interfaces/dtos/user-register-dto.interface';
import {environment} from "../shared/utils/environment";

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
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    return token !== null;
  }
}
