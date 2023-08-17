import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { v4 as uuidv4 } from 'uuid';

import { Todo } from '../models/todo.model';
import { environment } from 'src/environments';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
 
  constructor(private http: HttpClient) { }

  todoApi:string = `${environment.apiUrl}/api/todo`;

  getAllTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.todoApi);
  }


  addTodo(newTodo: Todo): Observable<Todo> {
    newTodo.id = uuidv4();
    return this.http.post<Todo>(this.todoApi, newTodo);
  }

  updateTodoCompletion(todo: Todo): Observable<string> {
    return this.http.put<string>(`${this.todoApi}/${todo.id}`, todo.isCompleted);
  }

  deleteTodo(todo: Todo): Observable<void> {
    return this.http.delete<void>(`${this.todoApi}/${todo.id}`);
  }
}
