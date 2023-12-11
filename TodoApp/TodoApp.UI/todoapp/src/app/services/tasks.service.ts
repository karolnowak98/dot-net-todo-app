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
  private editTaskUrl= this.tasksControllerUrl + 'edit-task';
  private updateTaskStatusUrl= this.tasksControllerUrl + 'update-task-status';

  constructor(private http: HttpClient) { }

  private getJwtHeaders(): HttpHeaders {
    const token = localStorage.getItem('jwtToken');
    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }

  getTasks(): Observable<ServiceResponse<GetTaskDto[]>> {
    return this.http.get<ServiceResponse<GetTaskDto[]>>(this.getUserTasksUrl, { headers: this.getJwtHeaders() });
  }

  updateTaskStatus(updateStatusTaskDto: UpdateStatusTaskDto): Observable<ServiceResponse<any>> {
    return this.http.put<ServiceResponse<any>>(this.updateTaskStatusUrl, updateStatusTaskDto, { headers: this.getJwtHeaders() });
  }

  editTask(taskDto: GetTaskDto): Observable<ServiceResponse<GetTaskDto>> {
    return this.http.post<ServiceResponse<GetTaskDto>>(this.editTaskUrl, taskDto, { headers: this.getJwtHeaders() });
  }
}
