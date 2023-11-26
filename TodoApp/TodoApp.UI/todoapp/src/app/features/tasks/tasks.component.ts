import { Component, OnInit } from '@angular/core';

import { GetTaskDto } from "../../shared/interfaces/dtos/get-task-dto.interface";
import { TasksService } from "../../services/tasks.service";
import { ServiceResponse } from "../../shared/interfaces/service-response.interface";

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit{
  tasks: GetTaskDto[] = [];

  constructor(private taskService: TasksService) { }

  ngOnInit(): void {
    this.taskService.getTasks().subscribe({
      next: (response: ServiceResponse<GetTaskDto[]>) => {
        if (response && response.success) {
          this.tasks = response.data || [];
        } else {

        }
      },
      error: (error) => {
        // Obsłuż błąd
      },
      complete: () => {
        // Obsłuż zakończenie (opcjonalne)
      }
    });
  }

  editTask(task : GetTaskDto) : void{

  }

  addTask(task : GetTaskDto) : void{

  }
}
