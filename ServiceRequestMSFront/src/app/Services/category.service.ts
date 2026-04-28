import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse, CategoryDto, CategoryItemDto, CreateCategoryDto, UpdateCategoryDto } from '../Models';

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

  createCategory(dto: CreateCategoryDto): Observable<CategoryItemDto> {
    return this.http
      .post<ApiResponse<CategoryItemDto>>(`${environment.apiUrl}/${this.apiUrl}/CreateCategory`, dto)
      .pipe(map((response) => response.data));
  }

  deleteCategory(categoryId: string): Observable<any> {
    return this.http
      .delete<ApiResponse<any>>(`${environment.apiUrl}/${this.apiUrl}/${categoryId}`)
      .pipe(map((response) => response));
  }

  updateCategory(dto: UpdateCategoryDto): Observable<CategoryItemDto> {
    return this.http
      .post<ApiResponse<CategoryItemDto>>(`${environment.apiUrl}/${this.apiUrl}/UpdateCategory`, dto)
      .pipe(map((response) => response.data));
  }
}
