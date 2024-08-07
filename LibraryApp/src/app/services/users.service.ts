import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Login } from '../models/login.model';
import { Register } from '../models/register.model';
import { User } from '../models/user.model';
import { jwtDecode as jwt_decode } from "jwt-decode";
import { DeleteAccountDto } from '../models/deleteaccount.model';
import { ChangePasswordDto } from '../models/changepassworddto.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseApiUrl: string = 'https://192.168.1.20:7299/api/Users/';
  private isLoggedIn: boolean = false;
  private userRole: string = '';
  private userId: string = '';
  constructor(private http: HttpClient, private router: Router) {
    this.userRole = localStorage.getItem('userRole') || '';
    this.userId = localStorage.getItem('userId') || '';
    this.isLoggedIn = !!localStorage.getItem('token');
  }

  setLoggedIn(value: boolean) {
    this.isLoggedIn = value;
  }

  isLoggedInUser(): boolean {
    return this.isLoggedIn;
  }

  getUserRole(): string {
    return this.userRole;
  }

  setUserRole(role: string) {
    this.userRole = role;
    localStorage.setItem('userRole', role);
  }

  getUserId(): string {
    return this.userId;
  }

  setUserId(userId: string) {
    this.userId = userId;
    localStorage.setItem('userId', userId);
  }

  login(loginData: Login): Observable<any> {
    return this.http.post<any>(`${this.baseApiUrl}login`, loginData)
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.token);
          this.setLoggedIn(true);
          const token = localStorage.getItem('token');
          if (token) {
            const decodedToken = jwt_decode(token) as { nameid: string, role: string };
            const userId = decodedToken.nameid;
            const userRole = decodedToken.role;
            this.setUserRole(userRole);
            this.setUserId(userId);
          }
        }),
        catchError(error => {
          console.error('Błąd logowania', error);
          throw error;
        })
      );
  }

  register(registerData: Register): Observable<any> {
    return this.http.post<any>(this.baseApiUrl + 'register', registerData)
      .pipe(
        catchError(error => {
          console.error('Błąd rejestracji', error);
          throw error;
        })
      );
  }

  logout(): void {
    localStorage.removeItem('token');
    this.setLoggedIn(false);
    localStorage.removeItem('userRole');
    localStorage.removeItem('userId');
    this.router.navigate(['/logout']);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<any>(`${this.baseApiUrl}`);
  }

  getUser(id: string): Observable<User> {
    return this.http.get<any>(`${this.baseApiUrl}${id}`);
  }

  updateUser(user: User): Observable<any> {
    return this.http.put<any>(`${this.baseApiUrl}${user.user_id}`, user);
  }

  deleteUser(id: string): Observable<any> {
    return this.http.delete<any>(`${this.baseApiUrl}${id}`);
  }

  changePassword(id: string, password: string, newPassword: string): Observable<any> {
    const body: ChangePasswordDto = { password, newPassword };
    return this.http.put<any>(`${this.baseApiUrl}${id}/changePassword`, body);
  }

  deleteAccount(id: string, password: string): Observable<any> {
    const body: DeleteAccountDto = { password };
    return this.http.delete<any>(`${this.baseApiUrl}${id}/deleteAccount`, { body });
  }
}
