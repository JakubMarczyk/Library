import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Book } from '../models/book.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class BooksService {

  baseApiUrl: string = 'https://192.168.1.20:7299/api/books/'

  constructor(private http: HttpClient) { }

  
  getBooks(queryParams: any): Observable<Book[]> {
    let params = new HttpParams();

    // Dodawanie parametrów tylko wtedy, gdy są dostarczone
    if (queryParams.authorIds) {
      params = params.set('authorIds', queryParams.authorIds);
    } else {
      params = params.set('authorIds', ",");
    }
    if (queryParams.categoryIds) {
      params = params.set('categoryIds', queryParams.categoryIds);
    } else {
      params = params.set('categoryIds', ",");
    }

    // Zapytanie HTTP GET z parametrami
    return this.http.get<Book[]>(this.baseApiUrl, { params });
  }
  

  getBook(id: string): Observable<Book> {
    return this.http.get<Book>(this.baseApiUrl + id)
  }

  addBook(book: Book): Observable<Book>{
    return this.http.post<Book>(this.baseApiUrl, book);
  }

  editBook(book: Book): Observable<Book>{
    return this.http.put<Book>(this.baseApiUrl + book.book_id, book)
  }

  deleteBook(id: string): Observable<Book>{
    return this.http.delete<Book>(this.baseApiUrl + id)
  }

  search(searchedText: string): Observable<Book[]> {
    return this.http.get<Book[]>(this.baseApiUrl + 'search/' + searchedText)
  }

  getBooksByBookshelf(bookshelfId: string): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.baseApiUrl}bookshelfBooks/${bookshelfId}`);
  }
}
