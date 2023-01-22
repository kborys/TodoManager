import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { GroupComponent } from './groups/group.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'sign-up', component: SignupComponent },
  {
    path: 'group/:id',
    component: GroupComponent,
    canActivate: [AuthGuard],
  },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
