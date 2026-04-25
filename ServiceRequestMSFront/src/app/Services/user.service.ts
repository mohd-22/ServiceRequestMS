import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse, UserDto, UserRegistrationDto } from '../Models';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'User';

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<UserDto[]> {
    return this.http
      .get<ApiResponse<UserDto[]>>(`${environment.apiUrl}/${this.apiUrl}/GetAllUsers`)
      .pipe(map((response) => response.data ?? []));
  }

  getPagedUsers(page: number, pageSize: number = 5): Observable<ApiResponse<UserDto[]>> {
    return this.http.get<ApiResponse<UserDto[]>>(
      `${environment.apiUrl}/${this.apiUrl}/PagedUser/${page}/${pageSize}`
    );
  }

  activateUser(userId: string): Observable<void> {
    return this.http
      .post<ApiResponse<unknown>>(`${environment.apiUrl}/${this.apiUrl}/ActiveUser/${userId}`, {})
      .pipe(map(() => void 0));
  }

  deactivateUser(userId: string): Observable<void> {
    return this.http
      .post<ApiResponse<unknown>>(`${environment.apiUrl}/${this.apiUrl}/DeactiveUser/${userId}`, {})
      .pipe(map(() => void 0));
  }

  addUser(request: UserRegistrationDto): Observable<void> {
    return this.http
      .post<ApiResponse<unknown>>(`${environment.apiUrl}/${this.apiUrl}/AddUser`, request)
      .pipe(map(() => void 0));
  }
}