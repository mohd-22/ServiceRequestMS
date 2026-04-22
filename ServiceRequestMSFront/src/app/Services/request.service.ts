import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApiResponse, RequestAdminDto } from '../Models';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  private apiUrl = 'Request'; 

  constructor(private http: HttpClient) { }

  getRequests(): Observable<RequestAdminDto[]> {
    return this.http
      .get<ApiResponse<RequestAdminDto[]>>(`${environment.apiUrl}/${this.apiUrl}/AdminReq`)
      .pipe(map((response) => response.data ?? []));
  }
}
