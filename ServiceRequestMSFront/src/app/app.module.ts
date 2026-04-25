import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RequestsComponent } from './Components/requests/requests.component';
import { DashboardHomeComponent } from './Components/dashboard-home/dashboard-home.component';
import { CategoriesComponent } from './Components/categories/categories.component';
import { LoginComponent } from './Components/login/login.component';
import { UsersComponent } from './Components/users/users.component';
import { ProfileComponent } from './Components/profile/profile.component';
import { AuthInterceptor } from './Interceptors/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    RequestsComponent,
    DashboardHomeComponent,
    CategoriesComponent,
    LoginComponent,
    UsersComponent,
    ProfileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
