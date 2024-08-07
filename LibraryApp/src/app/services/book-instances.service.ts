import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Book_Instance } from '../models/book_instance.model';

@Injectable({
  providedIn: 'root'
})
export class BookInstancesService {
  private apiUrl = 'https://192.168.1.20:7299/api/Book_Instance';
  constructor(private http: HttpClient) { }

  createBookInstance(bookInstance: Book_Instance): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, bookInstance).pipe(
      catchError(this.handleError)
    );
  }

  // PUT update book instance by ID
  updateBookInstance(id: string, bookInstance: Book_Instance): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, bookInstance).pipe(
      catchError(this.handleError)
    );
  }

  // DELETE book instance by ID
  deleteBookInstance(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }

  // Handle error
  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('An error occurred; please try again later.');
  }

  getBookId(id: string): Observable<string> {
    return this.http.get<any>(`${this.apiUrl}/getBookId/${id}`).pipe(
      catchError(this.handleError)
    );
  }
}
