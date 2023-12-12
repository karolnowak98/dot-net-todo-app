import { Component, OnInit } from '@angular/core';

import { UpdateStatusTaskDto } from "../../shared/interfaces/dtos/tasks/update-status-task-dto.interface";
import { GetTaskDto } from "../../shared/interfaces/dtos/tasks/get-task-dto.interface";
import { ServiceResponse} from "../../shared/interfaces/service-response.interface";
import { TasksService } from "../../services/tasks.service";
import { TaskStatus } from "../../shared/enums/task-status.enum";
import { catchError, switchMap } from "rxjs/operators";
import { of } from "rxjs";

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit{
  tasks: GetTaskDto[] = [];

  isUpdatingStatus = false;

  constructor(private taskService: TasksService) { }

  ngOnInit(): void {
    this.getTasks();
  }

  getTasks() : void{
    this.taskService.getTasks().subscribe({
      next: (response: ServiceResponse<GetTaskDto[]>) => {
        if (response && response.success) {
          this.tasks = response.data || [];
        } else {

        }
      },
      error: (error) => {
        // Obsłuż błąd
      }
    });
  }

  editTask(task : GetTaskDto) : void{

  }

  addTask(task : GetTaskDto) : void{

  }

  toggleTaskStatus(id: string, status: TaskStatus): void {

    if(this.isUpdatingStatus){
      return;
    }

    const taskIndex = this.tasks.findIndex(task => task.id === id);
    if (taskIndex !== -1) {
      this.tasks[taskIndex].status = status === TaskStatus.Completed ? TaskStatus.NotCompleted : TaskStatus.Completed;
      this.isUpdatingStatus = true;

      const updateTaskDto: UpdateStatusTaskDto = {
        id: id,
        status: this.tasks[taskIndex].status
      };

      this.taskService.updateTaskStatus(updateTaskDto).pipe(
        catchError((errorResponse) => {
          console.log(errorResponse.error);
          this.tasks[taskIndex].status = status;
          return of(null);
        }),
        switchMap(() => {
          this.isUpdatingStatus = false;
          return of(null);
        })
      ).subscribe({
        next: (response) => {
          if (response && response) {
            console.log("Success");
          }
        }
      });
    }
  }
}
