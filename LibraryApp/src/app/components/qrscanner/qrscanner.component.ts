import { Component, OnInit } from '@angular/core';
import { BarcodeFormat } from '@zxing/library';
import { ZXingScannerModule } from '@zxing/ngx-scanner';
import { BorrowsService } from '../../services/borrows.service';
import { UsersService } from '../../services/users.service';
import { ActivatedRoute, Router } from '@angular/router';
import { waitForAsync } from '@angular/core/testing';
import { InfoScannerService } from '../../services/info-scanner.service';
import { BookInstancesService } from '../../services/book-instances.service';
import { Observable, catchError, map, of } from 'rxjs';

interface ScanData {
  type: string;
  id: string;
}
interface ReturnData {
  user_id: string,
  book_instance_id: string;
  bookshelf_id: string;
}

interface BookData {
  user_id: string,
  book_instance_id: string;
}

interface transferData {
  user_id: string,
  borrow_id: string;
}

@Component({
  selector: 'app-qrscanner',
  templateUrl: './qrscanner.component.html',
  styleUrls: ['./qrscanner.component.css']
})
export class QRscannerComponent {
  formats: BarcodeFormat[] = [BarcodeFormat.QR_CODE];
  scannerMode: string = 'info';
  modeMessage: string = "Tryb Informacji";
  message: string = "";
  errorMessage: string = "";
  scannedData: string = "";
  scanData: ScanData = {
    type: '',
    id: ''
  };
  returnData: ReturnData = {
    user_id: '',
    book_instance_id: '',
    bookshelf_id: ''
  }
  bookData: BookData = {
    user_id: '',
    book_instance_id: ''
  }

  transferData: transferData = {
    user_id: '',
    borrow_id: ''
  }

  book_id = "";
  scannerEnabled: boolean = true;
  isLogged: boolean = false;
  constructor(private route: ActivatedRoute,
    private router: Router,
    private borrowsService: BorrowsService,
    private usersService: UsersService,
    private infoScannerService: InfoScannerService,
    private bookInstancesService: BookInstancesService
  ) { }

  ngOnInit() {
    this.updateMessage();
    this.isUserLogged();
  }

  isUserLogged() {
    this.isLogged = this.usersService.isLoggedInUser();
  }

  updateMessage() {
    switch (this.scannerMode) {
      case 'info':
        this.modeMessage = 'Tryb informacji:'
        this.message = 'Zeskanuj kod QR aby uzyskać informację.';
        this.errorMessage = '';
        break;
      case 'borrow':
        this.modeMessage = 'Tryb wypożyczania:'
        this.message = 'Zeskanuj kod QR ksiązki.';
        this.errorMessage = '';
        break;
      case 'return':
        this.modeMessage = 'Tryb zwrotu:'
        this.message = 'Zeskanuj kod QR ksiązki.';
        this.errorMessage = '';
        break;
      case 'return2':
        this.modeMessage = 'Tryb zwrotu:'
        this.message = 'Zeskanuj kod QR biblioteczki.';
        this.errorMessage = '';
        break;
      case 'transfer':
        this.modeMessage = 'Odbierz ksiązkę od innego użytkownika:'
        this.message = 'Zeskanuj kod QR transferu ksiązki.';
        this.errorMessage = '';
        break;
      default:
        this.message = '';
        this.errorMessage = '';
    }
  }

  camerasNotFoundHandler(event: any) {
    console.log('Nie znaleziono kamery', event);
  }

  async scanSuccessHandler(event: any) {
    this.scannerEnabled = false;
    this.scannedData = event;
    try {
      this.scanData = JSON.parse(this.scannedData);
    } catch (error) {
      console.error('Error parsing scanned data:', error);
    }

    const exists = this.scanData.type === "borrow" ? false : await this.objectExists().toPromise();
    switch (this.scannerMode) {
      case 'info':
            if (exists) {
              if (this.scanData.type == "book") {
                this.bookInstancesService.getBookId(this.scanData.id).subscribe({
                  next: (id) => {
                    this.book_id = id;
                    this.router.navigate(['/book', this.book_id]);
                  },
                  error: (response => {
                    this.errorMessage = response;
                  })
                });
              } else { this.router.navigate(['scanInfo', this.scanData.type, this.scanData.id]); }             
            } else {
              this.errorMessage = `Nie znaleziono kodu QR w bazie`;
              setTimeout(() => {
                this.changeScannerMode("info");
              }, 3000);
            }
          
        break;
      case 'borrow':
        if (this.scanData.type == "book") {
          if (exists) {
            this.bookData.book_instance_id = this.scanData.id;
            this.bookData.user_id = this.usersService.getUserId();
            this.borrowsService.borrowBook(this.bookData).subscribe({
              next: () => {
                this.message = "Wypożyczono ksiązkę";
                setTimeout(() => {
                  this.changeScannerMode("info");
                }, 3000);
              },
              error: (response => {
                this.errorMessage = response;
              })
            });
          } else {
            this.errorMessage = `Nie znaleziono książki w bazie`;
          }
        } else {
          this.errorMessage = `Nie zeskanowano kodu ksiazki`;
          setTimeout(() => {
            this.changeScannerMode("borrow");
          }, 3000);
        }
        break;
      case 'return':
        if (this.scanData.type == "book") {
          if (exists) {
            this.returnData.book_instance_id = this.scanData.id;
            this.message = "Zeskanowano książkę";
            setTimeout(() => {
              this.changeScannerMode("return2");
            }, 1000);
          } else {
            this.errorMessage = `Nie znaleziono książki w bazie`;
            setTimeout(() => {
              this.changeScannerMode("transfer");
            }, 3000);
          }
        } else {
          this.errorMessage = `Nie zeskanowano kodu ksiazki`;
          setTimeout(() => {
            this.changeScannerMode("return");
          }, 3000);
        }
        break;
      case 'return2':
        if (this.scanData.type == "bookshelf") {
          if (exists) {
          this.returnData.bookshelf_id = this.scanData.id;
          this.returnData.user_id = this.usersService.getUserId();
          this.borrowsService.returnBook(this.returnData).subscribe({
            next: () => {
              this.message = "Zwrócono ksiązkę";
              setTimeout(() => {
                this.changeScannerMode("info");
              }, 3000);
            },
            error: (response => {
              this.errorMessage = response;
              setTimeout(() => {
                this.changeScannerMode("return2");
              }, 3000);
            })
          });
        } else {
          this.errorMessage = `Nie znaleziono biblioteczki w bazie`;
        }
    } else {
      this.errorMessage = `Nie zeskanowano biblioteczki ksiazki`;
          setTimeout(() => {
            this.changeScannerMode("info");
      }, 3000);
    }
        break;
      case 'transfer':
        if (this.scanData.type == "borrow") {
          this.transferData.borrow_id = this.scanData.id;
          this.transferData.user_id = this.usersService.getUserId();
          console.log(this.transferData)
          this.borrowsService.transferBook(this.transferData).subscribe({
            next: () => {
              this.message = "Odebrano ksiązkę";
              setTimeout(() => {
                this.changeScannerMode("info");
              }, 3000);
            },
            error: (response => {
              this.errorMessage = response;
              setTimeout(() => {
                this.changeScannerMode("transfer");
              }, 3000);
            })
          });
    } else {
      this.errorMessage = `Nie zeskanowano kodu transferu`;
          setTimeout(() => {
          this.changeScannerMode("transfer");
      }, 3000);
      
    }
        break;
      default:
    }
  }

  scanErrorHandler(event: any) {
    console.error('Error during scan:', event);
  }

  changeScannerMode(mode: string) {
    this.scannerMode = mode;
    this.updateMessage();
    this.scannerEnabled = true;
  }

  objectExists(): Observable<boolean> {
    return this.infoScannerService.scanEntity(this.scanData).pipe(
      map((response) => {
        return response;
      }),
      catchError((error) => {
        console.error(error);
        return of(false);
      })
    );
  }
}
