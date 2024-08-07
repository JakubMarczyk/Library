import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
interface ScanData {
  type: string;
  id: string;
}

@Injectable({
  providedIn: 'root'
})
export class InfoScannerService {

  private apiUrl = 'https://192.168.1.20:7299/api/InfoScanner/Scan'; // Zaktualizuj URL do swojego API

  constructor(private http: HttpClient) { }

  scanEntity(scanDto: ScanData): Observable<boolean> {
    return this.http.post<boolean>(this.apiUrl, scanDto);
  }

}
