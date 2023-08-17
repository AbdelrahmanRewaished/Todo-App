import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from '../models/user.model';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private userSubject: BehaviorSubject<User | null>;
    public user: Observable<User | null>;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
        this.user = this.userSubject.asObservable();
    }

    public get userValue() {
        return this.userSubject.value;
    }

    persistUser(user: User) {
        localStorage.setItem('user', JSON.stringify(user));
        this.userSubject.next(user);
        return user;
    }

    login(email: string, password: string) { 
      return this.http.post<User>(`${environment.apiUrl}/account/login`, { email, password })
          .pipe(map(user => this.persistUser(user)));
    }

    register(userData: {firstName:string, lastName:string, email:string, phoneNumber:string, password:string, confirmPassword:string}) {
        return this.http.post<User>(`${environment.apiUrl}/account/register`, userData)
            .pipe(map(user => this.persistUser(user)));
    }

    logout() {
        localStorage.removeItem('user');
        this.userSubject.next(null);
        this.router.navigate(['/login']);
    }
}