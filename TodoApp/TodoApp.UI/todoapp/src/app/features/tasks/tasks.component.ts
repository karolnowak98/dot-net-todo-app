import {Component, OnInit} from '@angular/core';

import {UpdateStatusTaskDto} from "../../shared/interfaces/dtos/tasks/update-status-task-dto.interface";
import {GetTaskDto} from "../../shared/interfaces/dtos/tasks/get-task-dto.interface";
import {ServiceResponse} from "../../shared/interfaces/service-response.interface";
import {TasksService} from "../../services/tasks.service";
import {TaskStatus} from "../../shared/enums/task-status.enum";

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit{
  tasks: GetTaskDto[] = [];

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

  toggleTaskStatus(id: string, newStatus: TaskStatus): void {

    const updateTaskDto: UpdateStatusTaskDto = {
      id: id,
      status: newStatus === TaskStatus.Completed ? TaskStatus.NotCompleted : TaskStatus.Completed
    };

    this.taskService.updateTaskStatus(updateTaskDto).subscribe({
      next: (response) => {
        if (response && response) {
          console.log("Success");
          this.updateTaskStatusInList(id, updateTaskDto.status);
        }
      }
    });
  }

  updateTaskStatusInList(taskId: string, newStatus: TaskStatus): void {
    const taskIndex = this.tasks.findIndex(task => task.id === taskId);

    if (taskIndex !== -1) {
      this.tasks[taskIndex].status = newStatus;
    }
  }

  protected readonly TaskStatus = TaskStatus;
}
