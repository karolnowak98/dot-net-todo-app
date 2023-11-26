import { Injectable } from '@angular/core';

import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {ServiceResponse} from "../shared/interfaces/service-response.interface";
import {GetTaskDto} from "../shared/interfaces/dtos/get-task-dto.interface";
import {environment} from "../shared/utils/environment";

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  private tasksControllerUrl = environment.httpsUrl + 'tasks/';
  private getUserTasksUrl= this.tasksControllerUrl + 'get-all';
  private addTaskUrl= this.tasksControllerUrl + 'create-task';

  constructor(private http: HttpClient) { }

  getTasks(): Observable<ServiceResponse<GetTaskDto[]>> {
    const token = localStorage.getItem('jwtToken');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.get<ServiceResponse<GetTaskDto[]>>(this.getUserTasksUrl, { headers });
  }
}
