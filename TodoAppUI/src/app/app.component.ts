import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { User } from './models/user.model';
import { Role } from './models/role';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TodoApp.UI';
  user?: User | null;

  constructor(private AuthService: AuthService) {
      this.AuthService.user.subscribe(x => {
        this.user = x;
      });
  }

  get toShowNavbar() {
    const currentPath = window.location.pathname;
    const isAccountRelatedPage: boolean = currentPath === '/register' || currentPath === '/' || currentPath === '/login';
    return this.user !== null && !isAccountRelatedPage;
  }
  get isClient() {
    return this.user?.roles.includes(Role.Client);
  }

  logout() {
      this.AuthService.logout();
  }
}
