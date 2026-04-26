import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { DashboardHomeComponent } from './Components/dashboard-home/dashboard-home.component';
import { RequestsComponent } from './Components/requests/requests.component';
import { CategoriesComponent } from './Components/categories/categories.component';
import { UsersComponent } from './Components/users/users.component';
import { ProfileComponent } from './Components/profile/profile.component';
import { AdminGuard } from './Services/admin.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardHomeComponent },
  { path: 'requests', component: RequestsComponent },
  { path: 'categories', component: CategoriesComponent },
  { path: 'users', component: UsersComponent },
  { path: 'profile', component: ProfileComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
