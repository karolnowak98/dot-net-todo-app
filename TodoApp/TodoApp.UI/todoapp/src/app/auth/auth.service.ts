import { Injectable } from '@angular/core';
import { Router } from "@angular/router";
import { HttpClient } from '@angular/common/http';
import { map } from "rxjs";
import { Users } from '../shared/module/Users';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = "http://localhost:5259/api/";
  constructor(private http : HttpClient, private route : Router) { }

  login(values:any){
    return this.http.post<Users>(this.baseUrl + 'account/login', values).pipe(
      map(users => {
        localStorage.setItem('username',users.displayName);
      })
    )
  }

  logout(){
    localStorage.removeItem('username');
    this.route.navigateByUrl('/');
  }
}
