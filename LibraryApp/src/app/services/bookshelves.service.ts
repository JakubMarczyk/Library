import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Bookshelf } from '../models/bookshelf.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookshelvesService {
  baseApiUrl: string = 'https://192.168.1.20:7299/api/bookshelves/'

  constructor(private http: HttpClient) { }

  getBookshelves(): Observable<Bookshelf[]> {
    return this.http.get<Bookshelf[]>(this.baseApiUrl)
  }

  getBookshelf(id: string): Observable<Bookshelf> {
    return this.http.get<Bookshelf>(this.baseApiUrl + id)
  }

  addBookshelf(bookshelf: Bookshelf): Observable<Bookshelf> {
    return this.http.post<Bookshelf>(this.baseApiUrl, bookshelf)
  }

  editBookshelf(bookshelf: Bookshelf): Observable<Bookshelf> {
    return this.http.put<Bookshelf>(this.baseApiUrl + bookshelf.bookshelf_id, bookshelf)
  }

  deleteBookshelf(id: string): Observable<Bookshelf> {
    return this.http.delete<Bookshelf>(this.baseApiUrl + id)
  }

}
