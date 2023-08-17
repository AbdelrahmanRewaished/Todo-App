import { Component, OnInit } from '@angular/core';
import { Todo } from 'src/app/models/todo.model';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css']
})

export class TodosComponent implements OnInit{ 
  todos: Todo[] = [];
  loadingAddingTodo: boolean = false;
  loading: boolean = false;

  newTodo: Todo = {
    id: '',
    description: '',
    createdDate: new Date(),
    isCompleted: false,
    completedDate: new Date(),
  }
  constructor(private todoService: TodoService) {}
  
  ngOnInit(): void {
    this.getAllTodos();
  }

  getAllTodos(): void {
    this.loading = true;
    this.todoService.getAllTodos()
    .subscribe({
        next: (todos) => {
          this.todos = todos
          this.loading = false;
        },
        error: () => {
          this.loading = false;
        }
    })
  }

  addTodo(): void {
    this.loadingAddingTodo = true;
    this.todoService.addTodo(this.newTodo)
      .subscribe({
        next:(todo => {
            this.getAllTodos();
            this.loadingAddingTodo = false;
        }),
        error: () => {
          this.loadingAddingTodo = false;
        } 
      })
  }

  updateTodoCompletion(event: any, todo: Todo): void {
      todo.isCompleted = event.target.checked;
      this.loading = true;
      this.todoService.updateTodoCompletion(todo)
      .subscribe({
        next: ((updatedTodoId: string) => {
          console.log(`Todo with id = ${updatedTodoId} is updated successfully`)
          this.loading = false;
        }),
        error: () => {
          this.loading = false;
        }
      })
  }

  deleteTodo(todo: Todo) {
    this.loading = true;
    this.todoService.deleteTodo(todo)
    .subscribe({
      next: (() => {
        console.log(`Todo is deleted successfully`);
        this.getAllTodos();
        this.loading = false;
      }),
      error: () => {
        this.loading = false;
      }
    })
  }
}
