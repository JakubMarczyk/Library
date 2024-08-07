import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { BorrowsService } from '../../services/borrows.service';
import { BorrowedBook } from '../../models/borrowedBook.model';

@Component({
  selector: 'app-borrowed-book',
  templateUrl: './borrowed-book.component.html',
  styleUrls: ['./borrowed-book.component.css']
})
export class BorrowedBookComponent implements OnInit {
  id: string | null = "";
  defaultCover: string = "https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781787550360/classic-book-cover-foiled-journal-9781787550360_hr.jpg";
  book: any;
  showQRCode: boolean = false;
  qrData: string ="";
  constructor(private route: ActivatedRoute,
    private router: Router,
    private borrowsService: BorrowsService) { }


  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      if (params.get('id')) {
        this.id = params.get('id');
        if (this.id) {
          this.borrowsService.GetUserBorrowedBook(this.id)
            .subscribe({
              next: (book: BorrowedBook) => {
                this.book = book;
              }
            })
        }
      }
    })

  }
  extendReturnTime() {
    this.borrowsService.ExtendReturnTime(this.book.borrow_id).subscribe({
      next: () => {
        this.reloadPage();
      },
      error: (response => {
        console.log(response);
      })
    });
  }

  generateQRCode() {
    this.qrData = JSON.stringify({
      type: "borrow",
      id: this.book.borrow_id
    });
    this.showQRCode = !this.showQRCode;
    console.log(this.qrData)
  }

  reloadPage() {
    window.location.reload()
  }
}
