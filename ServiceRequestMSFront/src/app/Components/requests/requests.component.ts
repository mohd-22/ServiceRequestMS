import { Component } from '@angular/core';
import { RequestAdminDto } from '../../Models';
import { RequestService } from '../../Services/request.service';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent {
  requests: RequestAdminDto[] = [];
  isLoading = false;
  errorMessage = '';
  role: string | null = null;

  constructor(private requestService: RequestService, private authService: AuthService) { }

  ngOnInit(): void {
    this.loadRequests();
    this.role = this.authService.getUserRole();
  }

 


  loadRequests(): void {
    this.isLoading = true;
    this.errorMessage = '';
    console.log(this.authService.getToken());
    this.requestService.getRequests().subscribe({
      next: (data) => {
        this.requests = data;
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = 'Could not load requests. Please check API and CORS settings.';
        console.error('Error fetching requests:', error);
      }
    });
  }

  getStatusClass(status: string): string {
    switch ((status || '').toLowerCase()) {
      case 'new':
        return 'badge bg-primary';
      case 'assigned':
        return 'badge bg-info text-dark';
    
      case 'in progress':
        return 'badge bg-warning text-dark';
      case 'accepted':
        return 'badge bg-success';
      case 'rejected':
        return 'badge bg-danger';
      case 'paused':
        return 'badge bg-secondary';
      default:
        return 'badge bg-dark';
    }
  }

  formatDate(value: string): string {
    if (!value) {
      return '-';
    }

    const parsed = new Date(value);
    if (Number.isNaN(parsed.getTime())) {
      return value;
    }

    return parsed.toLocaleString();
  }
}
