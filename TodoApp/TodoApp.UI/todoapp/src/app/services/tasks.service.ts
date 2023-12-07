import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

import { ServiceResponse } from "../shared/interfaces/service-response.interface";
import { environment } from "../shared/utils/environment";
import { UpdateStatusTaskDto } from "../shared/interfaces/dtos/tasks/update-status-task-dto.interface";
import { GetTaskDto } from "../shared/interfaces/dtos/tasks/get-task-dto.interface";

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  private tasksControllerUrl = environment.httpsUrl + 'tasks/';
  private getUserTasksUrl= this.tasksControllerUrl + 'get-all';
  private addTaskUrl= this.tasksControllerUrl + 'create-task';
  private updateTaskStatusUrl= this.tasksControllerUrl + 'update-task-status';

  constructor(private http: HttpClient) { }

  getTasks(): Observable<ServiceResponse<GetTaskDto[]>> {
    const token = localStorage.getItem('jwtToken');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.get<ServiceResponse<GetTaskDto[]>>(this.getUserTasksUrl, { headers });
  }

  updateTaskStatus(updateStatusTaskDto: UpdateStatusTaskDto): Observable<ServiceResponse<any>> {
    const token = localStorage.getItem('jwtToken');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.put<ServiceResponse<any>>(this.updateTaskStatusUrl, updateStatusTaskDto, { headers });
  }
}
