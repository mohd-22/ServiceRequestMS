import { Component, OnInit } from '@angular/core';
import { UserDto, UserRegistrationDto, UserRoles } from 'src/app/Models';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: UserDto[] = [];
  displayedUsers: UserDto[] = [];
  isLoading = false;
  isAddingUser = false;
  isSubmittingAddUser = false;
  activeActionUserId: string | null = null;
  errorMessage = '';
  addUserErrorMessage = '';
  role: string | null = null;
  currentPage = 1;
  totalPages = 1;
  readonly pageSize = 5;
  readonly roleOptions: Array<{ label: string; value: UserRoles }> = [
    { label: 'Admin', value: UserRoles.Admin },
    { label: 'Manager', value: UserRoles.Manager },
    { label: 'Employee', value: UserRoles.Employee },
    { label: 'Staff', value: UserRoles.Staff }
  ];
  newUser: UserRegistrationDto = this.createEmptyUser();

  constructor(
    private userService: UserService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.role = this.authService.getUserRole();

    if (this.role !== 'Admin' && this.role !== 'Manager') {
      this.errorMessage = 'Only admins and managers can view the users list.';
      return;
    }   

    this.loadUsers();
  }

  loadUsers(page: number = this.currentPage): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.userService.getPagedUsers(page, this.pageSize).subscribe({
      next: (response) => {
        const data = response.data ?? [];
        const visibleUsers = this.role === 'Manager'
          ? data.filter((user) => user.role === 'Staff')
          : data;

        this.users = data;
        this.displayedUsers = visibleUsers;
        this.currentPage = response.page || page;
        this.totalPages = response.totalPages && response.totalPages > 0 ? response.totalPages : 1;
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = 'Could not load users. Please check API, CORS, and auth settings.';
        console.error('Error fetching users:', error);
      }
    });
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

  getRequestCount(user: UserDto): number {
    return user.userRequests ? user.userRequests.length : 0;
  }

  getDisplayedUserCount(): number {
    return this.displayedUsers.length;
  }

  goToNextPage(): void {
    if (this.isLoading || this.currentPage >= this.totalPages) {
      return;
    }

    this.loadUsers(this.currentPage + 1);
  }

  goToPreviousPage(): void {
    if (this.isLoading || this.currentPage <= 1) {
      return;
    }

    this.loadUsers(this.currentPage - 1);
  }

  getStatusClass(isActive: boolean): string {
    return isActive ? 'badge bg-success' : 'badge bg-secondary';
  }

  canManageUserStatus(): boolean {
    return this.role === 'Admin';
  }

  canAddUser(): boolean {
    return this.role === 'Admin';
  }

  toggleUserStatus(user: UserDto): void {
    if (!this.canManageUserStatus() || !user?.id || this.activeActionUserId) {
      return;
    }

    this.activeActionUserId = user.id;
    const request$ = user.isActive
      ? this.userService.deactivateUser(user.id)
      : this.userService.activateUser(user.id);

    request$.subscribe({
      next: () => {
        user.isActive = !user.isActive;
        this.activeActionUserId = null;
      },
      error: (error) => {
        this.errorMessage = 'Could not update user status.';
        this.activeActionUserId = null;
        console.error('Error updating user status:', error);
      }
    });
  }

  isUserActionLoading(userId: string): boolean {
    return this.activeActionUserId === userId;
  }

  openAddUserForm(): void {
    if (!this.canAddUser()) {
      return;
    }

    this.addUserErrorMessage = '';
    this.newUser = this.createEmptyUser();
    this.isAddingUser = true;
  }

  closeAddUserForm(): void {
    this.isAddingUser = false;
    this.isSubmittingAddUser = false;
    this.addUserErrorMessage = '';
    this.newUser = this.createEmptyUser();
  }

  submitAddUser(): void {
    if (!this.canAddUser() || this.isSubmittingAddUser) {
      return;
    }

    this.isSubmittingAddUser = true;
    this.addUserErrorMessage = '';

    this.userService.addUser(this.newUser).subscribe({
      next: () => {
        this.isSubmittingAddUser = false;
        this.closeAddUserForm();
        this.loadUsers(1);
      },
      error: (error) => {
        this.isSubmittingAddUser = false;
        this.addUserErrorMessage = 'Could not add user. Please check input values.';
        console.error('Error adding user:', error);
      }
    });
  }

  private createEmptyUser(): UserRegistrationDto {
    return {
      fullName: '',
      userName: '',
      email: '',
      role: UserRoles.Employee,
      password: '',
      phoneNumber: ''
    };
  }
}