import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginDto } from '../../Models';
import { AuthService } from '../../Services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  credentials: LoginDto = {
    userName: '',
    password: ''
  };

  isLoading = false;
  showPassword = false;
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    if (this.authService.hasToken()) {
      this.router.navigate(['/dashboard']);
    }
  }

  submitLogin(): void {
    if (this.isLoading) {
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.authService.login(this.credentials).subscribe({

      next: (token) => {
        const normalizedToken = (token || '').replace(/^"|"$/g, '').trim();
        console.log("Token is Here:" + normalizedToken);
        this.authService.setToken(normalizedToken);
        this.isLoading = false;
        this.router.navigate(['/dashboard']);
      },
      error: (error: HttpErrorResponse) => {
        console.log('Original Error:', error);  
        this.isLoading = false;
        this.errorMessage = this.resolveErrorMessage(error);
      }
    });
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

 private resolveErrorMessage(error: any): string {
  
  if (error.status === 0) {
    return 'Server is unreachable. Please check if the API is running.';
  }

    
  const body = error.error;

  if (body && typeof body === 'object' && body.message) {
    return body.message; 
  }

  
  if (typeof body === 'string') {
    try {
      const parsed = JSON.parse(body);
      if (parsed.message) return parsed.message;
    } catch {
      return body; 
    }
  }

  return 'An unexpected error occurred.';
}
}