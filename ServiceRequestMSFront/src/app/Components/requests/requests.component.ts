import { Component } from '@angular/core';
import { CategoryItemDto, CreateRequestDto, RequestAdminDto, UserDto } from '../../Models';
import { RequestService } from '../../Services/request.service';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent {
  requests: any[] = [];
  staffUsers: UserDto[] = [];
  categories: CategoryItemDto[] = [];
  categoryItems: CategoryItemDto[] = [];
  isLoading = false;
  isLoadingStaff = false;
  isLoadingCategories = false;
  errorMessage = '';
  staffErrorMessage = '';
  addRequestErrorMessage = '';
  addRequestSuccessMessage = '';
  role: string | null = null;
  id: string | null = null;
  currentPage = 1;
  totalPages = 1;
  readonly pageSize = 5;
  isAddingRequest = false;
  isSubmittingAddRequest = false;
  selectedCategoryId = '';
  activeAssignRequestId: string | null = null;
  activeStatusRequestId: string | null = null;
  selectedStaffByRequestId: Record<string, string> = {};
  selectedNextStatusByRequestId: Record<string, string> = {};
  newRequest: CreateRequestDto = {
    title: '',
    description: '',
    categoryItemId: ''
  };

  constructor(
    private requestService: RequestService,
    private userService: UserService,
    private authService: AuthService,
    private categoryService: CategoryService
  ) { }

  ngOnInit(): void {
    this.role = this.authService.getUserRole();
    this.id = this.authService.getUserId();

    if (this.role === 'Admin' || this.role === 'Manager') {
      this.loadStaffUsers();
      this.currentPage = 1;
    }

    if (this.role === 'Employee') {
      this.loadCategories();
    }

    this.loadRequests();
  }

  loadRequests(page: number = this.currentPage): void {
    this.isLoading = true;
    this.errorMessage = '';

    if (this.role === 'Admin' || this.role === 'Manager') {
      this.requestService.getPagedRequests(page, this.pageSize).subscribe({
        next: (response) => {
          const data = response.data ?? [];

          this.requests = data;
          this.currentPage = response.page || page;
          this.totalPages = response.totalPages && response.totalPages > 0 ? response.totalPages : 1;
          this.isLoading = false;
        },
        error: (error) => {
          this.isLoading = false;
          this.errorMessage = 'Could not load requests. Please check API and CORS settings.';
          console.error('Error fetching requests:', error);
        }
      });
      return;
    }

    if (this.role === 'Employee') {
      this.requestService.getRequestsForEmployee(this.id).subscribe({
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
      return;
    }

    if (this.role === 'Staff') {
      this.requestService.getRequestsForStaff(this.id).subscribe({
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
      return;
    }

    this.isLoading = false;
  }

  canUsePaging(): boolean {
    return this.role === 'Admin' || this.role === 'Manager';
  }

  canAddRequest(): boolean {
    return this.role === 'Employee';
  }

  openAddRequestForm(): void {
    if (!this.canAddRequest()) {
      return;
    }

    this.addRequestErrorMessage = '';
    this.addRequestSuccessMessage = '';
    this.isAddingRequest = true;

    if (this.categories.length === 0 && !this.isLoadingCategories) {
      this.loadCategories();
    }
  }

  closeAddRequestForm(): void {
    this.isAddingRequest = false;
    this.addRequestErrorMessage = '';
    this.resetAddRequestForm();
  }

  onCategoryChange(): void {
    this.newRequest.categoryItemId = '';
    this.categoryItems = [];

    if (!this.selectedCategoryId) {
      return;
    }

    this.isLoadingCategories = true;
    this.addRequestErrorMessage = '';

    this.categoryService.getCategoryById(this.selectedCategoryId).subscribe({
      next: (category) => {
        this.categoryItems = category.items ?? [];
        this.isLoadingCategories = false;
      },
      error: (error: unknown) => {
        this.isLoadingCategories = false;
        this.addRequestErrorMessage = 'Could not load items for selected category.';
        console.error('Error loading category items:', error);
      }
    });
  }

  submitAddRequest(): void {
    if (!this.canAddRequest() || this.isSubmittingAddRequest) {
      return;
    }

    const payload: CreateRequestDto = {
      title: this.newRequest.title.trim(),
      description: this.newRequest.description.trim(),
      categoryItemId: this.newRequest.categoryItemId
    };

    if (!payload.title || !payload.description || !payload.categoryItemId) {
      this.addRequestErrorMessage = 'Please complete all required fields.';
      return;
    }

    this.isSubmittingAddRequest = true;
    this.addRequestErrorMessage = '';
    this.addRequestSuccessMessage = '';

    this.requestService.createRequest(payload).subscribe({
      next: () => {
        this.isSubmittingAddRequest = false;
        this.addRequestSuccessMessage = 'Request added successfully.';
        this.isAddingRequest = false;
        this.resetAddRequestForm();
        this.loadRequests(1);
      },
      error: (error: unknown) => {
        this.isSubmittingAddRequest = false;
        this.addRequestErrorMessage = 'Could not add request. Please try again.';
        console.error('Error adding request:', error);
      }
    });
  }

  goToNextPage(): void {
    if (!this.canUsePaging() || this.isLoading || this.currentPage >= this.totalPages) {
      return;
    }

    this.loadRequests(this.currentPage + 1);
  }

  goToPreviousPage(): void {
    if (!this.canUsePaging() || this.isLoading || this.currentPage <= 1) {
      return;
    }

    this.loadRequests(this.currentPage - 1);
  }

  loadStaffUsers(): void {
    this.isLoadingStaff = true;
    this.staffErrorMessage = '';

    this.userService.getAllUsers().subscribe({
      next: (data: UserDto[]) => {
        this.staffUsers = data.filter((user) => user.role === 'Staff' && user.isActive);
        this.isLoadingStaff = false;
      },
      error: (error: unknown) => {
        this.isLoadingStaff = false;
        this.staffErrorMessage = 'Could not load active staff users.';
        console.error('Error fetching staff users:', error);
      }
    });
  }

  canAssignRequest(request: RequestAdminDto): boolean {
    const status = this.normalizeStatus(request.status);
    return this.role === 'Manager' && !request.assignedStaffId && (status === 'new' || status === 'paused');
  }

  getStaffStatusOptions(request: any): Array<{ label: string; value: string }> {
    const status = this.normalizeStatus(request.status);

    if (status === 'assigned') {
      return [{ label: 'In Progress', value: 'InProgress' }];
    }

    if (status === 'inprogress') {
      return [
        { label: 'Accepted', value: 'Accepted' },
        { label: 'Rejected', value: 'Rejected' }
      ];
    }

    return [];
  }

  canChangeStaffStatus(request: any): boolean {
    return this.role === 'Staff' && this.getStaffStatusOptions(request).length > 0;
  }

  assignStaff(request: RequestAdminDto): void {
    const staffId = this.selectedStaffByRequestId[request.id];
    if (!staffId || this.activeAssignRequestId) {
      return;
    }

    this.activeAssignRequestId = request.id;
    this.errorMessage = '';

    this.requestService.assignStaffToRequest(request.id, staffId).subscribe({
      next: () => {
        this.activeAssignRequestId = null;
        this.selectedStaffByRequestId[request.id] = '';
        this.loadRequests(this.currentPage);
      },
      error: (error: unknown) => {
        this.activeAssignRequestId = null;
        this.errorMessage = 'Could not assign request to staff.';
        console.error('Error assigning request to staff:', error);
      }
    });
  }

  isAssignLoading(requestId: string): boolean {
    return this.activeAssignRequestId === requestId;
  }

  changeStaffStatus(request: any): void {
    const nextStatus = this.selectedNextStatusByRequestId[request.id];
    if (!nextStatus || this.activeStatusRequestId) {
      return;
    }

    this.activeStatusRequestId = request.id;
    this.errorMessage = '';

    const staffNotes = nextStatus === 'Rejected'
      ? (this.selectedNextStatusByRequestId[`${request.id}-notes`] || '')
      : undefined;

    this.requestService.updateStaffRequestStatus(request.id, nextStatus, staffNotes).subscribe({
      next: () => {
        this.activeStatusRequestId = null;
        this.selectedNextStatusByRequestId[request.id] = '';
        this.selectedNextStatusByRequestId[`${request.id}-notes`] = '';
        this.loadRequests(this.currentPage);
      },
      error: (error: unknown) => {
        this.activeStatusRequestId = null;
        this.errorMessage = 'Could not update request status.';
        console.error('Error updating request status:', error);
      }
    });
  }

  isStatusLoading(requestId: string): boolean {
    return this.activeStatusRequestId === requestId;
  }

  getStatusClass(status: string): string {
    switch (this.normalizeStatus(status)) {
      case 'new':
        return 'badge bg-primary';
      case 'assigned':
        return 'badge bg-info text-dark';

      case 'inprogress':
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

  private loadCategories(): void {
    this.isLoadingCategories = true;
    this.addRequestErrorMessage = '';

    this.categoryService.getAllCategories().subscribe({
      next: (data) => {
        this.categories = data;
        this.isLoadingCategories = false;
      },
      error: (error: unknown) => {
        this.isLoadingCategories = false;
        this.addRequestErrorMessage = 'Could not load categories for new request.';
        console.error('Error loading categories:', error);
      }
    });
  }

  private resetAddRequestForm(): void {
    this.selectedCategoryId = '';
    this.categoryItems = [];
    this.newRequest = {
      title: '',
      description: '',
      categoryItemId: ''
    };
  }

  private normalizeStatus(status: string | null | undefined): string {
    return (status || '').replace(/\s+/g, '').toLowerCase();
  }
}
