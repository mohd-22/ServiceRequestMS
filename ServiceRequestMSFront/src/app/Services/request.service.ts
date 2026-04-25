import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApiResponse, CreateRequestDto, RequestAdminDto, RequestForEmployeeDto, RequestForStaffDto } from '../Models';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  private apiUrl = 'Request'; 

  constructor(private http: HttpClient) { }

  getRequestsForAdmin(): Observable<RequestAdminDto[]> {
    return this.http
      .get<ApiResponse<RequestAdminDto[]>>(`${environment.apiUrl}/${this.apiUrl}/AdminReq`)
      .pipe(map((response) => response.data ?? []));
  }

  getPagedRequests(page: number, pageSize: number = 5): Observable<ApiResponse<RequestAdminDto[]>> {
    return this.http.get<ApiResponse<RequestAdminDto[]>>(`${environment.apiUrl}/${this.apiUrl}/${page}/${pageSize}`);
  }
  getRequestsForEmployee(id : string | null): Observable<RequestForEmployeeDto[]> {
    return this.http
      .get<ApiResponse<RequestForEmployeeDto[]>>(`${environment.apiUrl}/${this.apiUrl}/EmpReq/${id}`)
      .pipe(map((response) => response.data ?? []));
  }
  getRequestsForStaff(id : string | null): Observable<RequestForStaffDto[]> {
    return this.http
      .get<ApiResponse<RequestForStaffDto[]>>(`${environment.apiUrl}/${this.apiUrl}/StaffReq/${id}`)
      .pipe(map((response) => response.data ?? []));
  }

  createRequest(payload: CreateRequestDto): Observable<void> {
    return this.http
      .post<ApiResponse<CreateRequestDto>>(`${environment.apiUrl}/${this.apiUrl}/CreateRequest`, payload)
      .pipe(map(() => void 0));
  }

  assignStaffToRequest(requestId: string, staffId: string): Observable<void> {
    return this.http
      .post<ApiResponse<unknown>>(`${environment.apiUrl}/Staff/AssignStaff`, null, {
        params: {
          RequestId: requestId,
          StaffId: staffId
        }
      })
      .pipe(map(() => void 0));
  }

  updateStaffRequestStatus(requestId: string, nextStatus: string, staffNotes?: string): Observable<void> {
    return this.http
      .post<ApiResponse<unknown>>(`${environment.apiUrl}/Staff/UpdateStaffRequestStatus`, null, {
        params: {
          requestId,
          nextStatus,
          ...(staffNotes ? { staffNotes } : {})
        }
      })
      .pipe(map(() => void 0));
  }
  // getRequestsForAdmin(): Observable<RequestAdminDto[]> {
  //   return this.http
  //     .get<ApiResponse<RequestAdminDto[]>>(`${environment.apiUrl}/${this.apiUrl}/AdminReq`)
  //     .pipe(map((response) => response.data ?? []));
  // }
}
