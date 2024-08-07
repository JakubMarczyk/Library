import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BorrowedBook } from '../models/borrowedBook.model';
import { Observable, catchError, throwError } from 'rxjs';
import { UsersService } from './users.service';
import { Borrow } from '../models/borrow.model';

interface BorrowDto {
  user_id: string,
  book_instance_id: string;
}

interface TransferDto {
  user_id: string,
  borrow_id: string;
}

interface ReturnDto {
  user_id: string,
  book_instance_id: string;
  bookshelf_id: string;
}

@Injectable({
  providedIn: 'root'
})
export class BorrowsService {
  baseApiUrl: string = 'https://192.168.1.20:7299/api/borrows/'

  constructor(private http: HttpClient, private usersService: UsersService) { }

  getUserBorrowed(id: string): Observable<BorrowedBook[]> {
    return this.http.get<BorrowedBook[]>(this.baseApiUrl + 'User/Borrowed/' + id)
      .pipe(catchError(this.handleError));
  }

  GetUserBorrowedBook(id: string): Observable<BorrowedBook> {
    return this.http.get<BorrowedBook>(this.baseApiUrl + 'User/Borrowed/Book/' + id)
  }

  ExtendReturnTime(id: string): Observable<void> {
    return this.http.put<void>(this.baseApiUrl + 'ExtendReturnTime/' + id, {})
  }

  borrowBook(borrowDto: BorrowDto): Observable<Borrow> {
    return this.http.post<Borrow>(this.baseApiUrl + 'BorrowBook', borrowDto).pipe(catchError(this.handleError));
  }

  returnBook(returnDto: ReturnDto): Observable<void> {
    return this.http.put<void>(this.baseApiUrl + 'ReturnBook', returnDto).pipe(catchError(this.handleError));
  }

  transferBook(transferDto: TransferDto): Observable<void> {
    return this.http.put<void>(this.baseApiUrl + 'TransferBook', transferDto).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred.
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      switch (error.status) {
        case 409:
          errorMessage = error.error || 'Książka jest już wypożyczona przez nowego użytkownika.';
          break;
        case 400:
          errorMessage = error.error || 'Nieprawidłowe żądanie.';
          break;
        case 500:
          errorMessage = 'Wystąpił błąd po stronie serwera. Spróbuj ponownie później.';
          break;
        default:
          errorMessage = `Server returned code: ${error.status}, error message is: ${error.message}`;
          break;
      }
    }
    return throwError(errorMessage);
  }
}
