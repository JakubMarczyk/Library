import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Author} from '../models/author.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorsService {

  baseApiUrl: string = 'https://192.168.1.20:7299/api'

  constructor(private http: HttpClient) { }

  getAuthors(): Observable<Author[]> {
    return this.http.get<Author[]>(this.baseApiUrl + '/authors')
  }

  getAuthor(id: string): Observable<Author> {
    return this.http.get<Author>(this.baseApiUrl + '/authors/' + id)
  }

  addAuthor(author: Author): Observable<Author>{
    return this.http.post<Author>(this.baseApiUrl + '/authors', author)
  }

  editAuthor(author: Author): Observable<Author>{
    return this.http.put<Author>(this.baseApiUrl + '/authors/' + author.author_id, author)
  }

  deleteAuthor(id: string): Observable<Author>{
    return this.http.delete<Author>(this.baseApiUrl + '/authors/' + id)
  }
}
