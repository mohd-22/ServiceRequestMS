import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  fullName = '';
  userName = '';
  email = '';
  role = '';
  userId = '';

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.fullName = this.authService.getFullName() || '-';
    this.userName = this.authService.getUserName() || '-';
    this.email = this.authService.getUserEmail() || '-';
    this.role = this.authService.getUserRole() || '-';
    this.userId = this.authService.getUserId() || '-';
  }
}
