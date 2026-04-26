import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse, ItemDto, UpdateCategoryDto } from '../Models';

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  private apiUrl = 'Item';

  constructor(private http: HttpClient) { }

  createItem(dto: ItemDto): Observable<ItemDto> {
    return this.http
      .post<ApiResponse<ItemDto>>(`${environment.apiUrl}/${this.apiUrl}/CreateItem`, dto)
      .pipe(map((response) => response.data));
  }

  deleteItem(itemId: string): Observable<any> {
    return this.http
      .delete<ApiResponse<any>>(`${environment.apiUrl}/${this.apiUrl}/${itemId}`)
      .pipe(map((response) => response));
  }

  updateItem(dto: UpdateCategoryDto): Observable<any> {
    return this.http
      .post<ApiResponse<any>>(`${environment.apiUrl}/${this.apiUrl}/UpdateItems`, dto)
      .pipe(map((response) => response));
  }
}