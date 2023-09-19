import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Book } from '../models/book.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class BooksService {

  baseApiUrl: string = 'https://localhost:7299/api'

  constructor(private http: HttpClient) { }

  
  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.baseApiUrl + '/books')
  }

  getBook(id: number): Observable<Book> {
    return this.http.get<Book>(this.baseApiUrl + '/books/' + id)
  }

  addBook(book: Book): Observable<Book>{
    return this.http.post<Book>(this.baseApiUrl + '/books', book);
  }

  editBook(book: Book): Observable<Book>{
    return this.http.put<Book>(this.baseApiUrl + '/books/' + book.book_id, book)
  }

  deleteBook(id: number): Observable<Book>{
    return this.http.delete<Book>(this.baseApiUrl + '/books/' + id)
  }
}
