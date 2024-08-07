import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Status } from '../models/status.model';

@Injectable({
  providedIn: 'root'
})
export class StatusesService {
  baseApiUrl: string = 'https://192.168.1.20:7299/api/status/'

  constructor(private http: HttpClient) { }

  getStatuses(): Observable<Status[]> {
    return this.http.get<Status[]>(this.baseApiUrl)
  }
}
