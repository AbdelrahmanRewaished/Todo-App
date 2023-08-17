import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TodosComponent } from './components/todos/todos.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './Guard/auth.guard';
import { AdminComponent } from './components/admin/admin.component';
import { ClientHomeComponent } from './components/home/home.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { Role } from './models/role';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path:'todos', component: TodosComponent, canActivate: [AuthGuard], data:{roles: [Role.Client] }},
  { path: 'home', component: ClientHomeComponent, canActivate: [AuthGuard], data:{roles: [Role.Admin, Role.Client] }},
  {path: 'register',  component: RegistrationComponent},
  { path: 'admin', component: AdminComponent, canActivate: [AuthGuard], data:{roles: [Role.Admin]} },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

