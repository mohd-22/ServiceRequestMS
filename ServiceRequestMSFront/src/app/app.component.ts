import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './Services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Service Request Management';
  isSidebarCollapsed = false;
  role: string | null = null;

  constructor(private router: Router, private authService: AuthService) {
    this.role = authService.getUserRole();
  }

  toggleSidebar(): void {
    this.isSidebarCollapsed = !this.isSidebarCollapsed;
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  isAuthRoute(): boolean {
    return this.router.url.startsWith('/login');
  }
}
