import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

import { UserLoginDto } from "../interfaces/dtos/user-login-dto.interface";
import { ServiceResponse } from "../interfaces/service-response.interface";
import { environment } from "../utils/environment";
import { UserRegisterDto } from "../interfaces/dtos/user-register-dto.interface";
import { UserTokensInterface } from "../interfaces/dtos/user-tokens.interface";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiEndpoints = {
    login: 'auth/login',
    register: 'auth/register'
  };

  private readonly jwtToken = 'jwtToken';
  private readonly refreshToken = 'refreshToken';

  constructor(private http: HttpClient, private router: Router) {}

  login(loginDto: UserLoginDto): Observable<ServiceResponse<UserTokensInterface>> {
    return this.http.post<ServiceResponse<UserTokensInterface>>(environment.httpsUrl + this.apiEndpoints.login, loginDto)
      .pipe(
        tap(response => {
          if (response.success && response.data) {
            localStorage.setItem(this.jwtToken, response.data.jwtToken);
            localStorage.setItem(this.refreshToken, response.data.refreshToken);
          }
        })
      );
  }

  register(registerDto: UserRegisterDto): Observable<ServiceResponse<any>> {
    return this.http.post<ServiceResponse<any>>(environment.httpsUrl + this.apiEndpoints.register, registerDto);
  }

  logout() {
    localStorage.removeItem(this.jwtToken);
    localStorage.removeItem(this.refreshToken);
    this.router.navigateByUrl('/');
  }

  getJwtToken(): string | null {
    return localStorage.getItem(this.jwtToken);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(this.refreshToken);
  }

  isLoggedIn(): boolean {
    const token = this.getJwtToken();
    return token !== null;
  }
}
