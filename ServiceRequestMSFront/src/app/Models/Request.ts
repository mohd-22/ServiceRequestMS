// Enums mirrored from backend enums in ServiceRequestMS.core/Models/Enums.
export enum RequestStatus {
    New = 1,
    Assigned = 2,
    InProgress = 3,
    Accepted = 4,
    Rejected = 5,
    Paused = 6,
}

export enum UserRoles {
    Admin = 1,
    Manager = 2,
    Employee = 3,
    Staff = 4,
}

export interface CategoryItemDto {
    id: string;
    name: string;
    description: string;
}

export interface CategoryDto {
    id: string;
    name: string;
    description: string;
    items: CategoryItemDto[];
}

export interface CreateCategoryDto {
    name: string;
    description: string;
}

export interface UpdateCategoryDto {
    id: string;
    name: string;
    description: string;
}

export interface ItemDto {
    name: string;
    description: string;
    categoryId: string;
}

export interface LoginDto {
    userName: string;
    password: string;
}

export interface CreateRequestDto {
    title: string;
    description: string;
    categoryItemId: string;
}

export interface UpdateEmployeeRequestDto {
    id: string;
    title: string;
    description: string;
    categoryItemId: string;
}

export interface RequestDto {
    id: string;
    title: string;
    status: RequestStatus;
}

export interface RequestAdminDto {
    id: string;
    title: string;
    description: string;
    status: string;
    requesterId: string;
    requesterName: string;
    assignedStaffId?: string | null;
    assignedStaffName?: string | null;
    categoryId: string;
    categoryName: string;
    itemName: string;
    createdDate: string;
}

export interface RequestForEmployeeDto {
    id: string;
    title: string;
    status: string;
    categoryName: string;
    itemName: string;
    assignedStaffName?: string | null;
    createdDate: string;
}

export interface RequestForStaffDto {
    id: string;
    title: string;
    description: string;
    status: string;
    requesterName: string;
    requesterPhone: string;
    categoryName: string;
    itemName: string;
    createdDate: string;
}

export interface UpdateUserDto {
    fullName: string;
    email: string;
    phoneNumber: string;
}

export interface UserDto {
    id: string;
    fullName: string;
    userName: string;
    email: string;
    phoneNumber: string;
    role: string;
    isActive: boolean;
    userRequests: RequestDto[];
    createdAt: string;
}

export interface UserRegistrationDto {
    fullName: string;
    userName: string;
    email: string;
    role: UserRoles;
    password: string;
    phoneNumber: string;
}

export interface ApiResponse<T> {
    success: boolean;
    message: string;
    page: number;
    totalPages?: number;
    data: T;
    errors?: string[] | null;
}

export interface CommentReadDto {
    id: string;
    text: string;
    createdAt: string;
    userName: string;
    userRole: string;
    userId: string;
}

export interface CreateCommentDto {
    text: string;
    requestId: string;
}