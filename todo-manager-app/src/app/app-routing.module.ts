import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { GroupsComponent } from './groups/groups.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { TodosComponent } from './todos/todos.component';

const appRoutes: Routes = [
  { path: 'todos', component: TodosComponent, canActivate: [AuthGuard] },
  { path: 'groups', component: GroupsComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'sign-up', component: SignupComponent },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
