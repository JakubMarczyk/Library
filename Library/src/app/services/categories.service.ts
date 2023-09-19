import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category} from '../models/category.model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  baseApiUrl: string = 'https://localhost:7299/api'

  constructor(private http: HttpClient) { }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseApiUrl + '/categories')
  }

  getCategory(id: number): Observable<Category> {
    return this.http.get<Category>(this.baseApiUrl + '/categories/' + id)
  }

  addCategory(category: Category): Observable<Category>{
    return this.http.post<Category>(this.baseApiUrl + '/categories', category)
  }

  editCategory(category: Category): Observable<Category>{
    return this.http.put<Category>(this.baseApiUrl + '/categories/' + category.category_id, category)
  }

  deleteCategory(id: number): Observable<Category>{
    return this.http.delete<Category>(this.baseApiUrl + '/categories/' + id)
  }
}
