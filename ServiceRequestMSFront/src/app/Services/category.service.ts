import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse, CategoryDto, CategoryItemDto } from '../Models';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = 'Category';

  constructor(private http: HttpClient) { }

  getAllCategories(): Observable<CategoryItemDto[]> {
    return this.http
      .get<ApiResponse<CategoryItemDto[]>>(`${environment.apiUrl}/${this.apiUrl}/GetAllCategories`)
      .pipe(map((response) => response.data ?? []));
  }

  getCategoryById(categoryId: string): Observable<CategoryDto> {
    return this.http
      .get<ApiResponse<CategoryDto>>(`${environment.apiUrl}/${this.apiUrl}/GetCategoryById/${categoryId}`)
      .pipe(map((response) => response.data));
  }
}
