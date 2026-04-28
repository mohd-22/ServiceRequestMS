import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApiResponse, CommentReadDto, CreateCommentDto } from '../Models';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private apiUrl = 'Comments';

  constructor(private http: HttpClient) { }

  getAllComments(requestId: string): Observable<CommentReadDto[]> {
    return this.http
      .get<ApiResponse<CommentReadDto[]>>(`${environment.apiUrl}/${this.apiUrl}/${requestId}`)
      .pipe(map((response) => response.data ?? []));
  }

  createComment(comment: CreateCommentDto): Observable<void> {
    return this.http
      .post<ApiResponse<CreateCommentDto>>(`${environment.apiUrl}/${this.apiUrl}`, comment)
      .pipe(map(() => void 0));
  }
}