import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { BooksService } from 'src/app/services/books.service';


@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {
  id: string | null = "";
  defaultCover: string = "https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781787550360/classic-book-cover-foiled-journal-9781787550360_hr.jpg";


  book: any;
  constructor(private route: ActivatedRoute,
    private router: Router,
    private booksService: BooksService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      if (params.get('id')) {
        this.id = params.get('id');
        if (this.id) {
          this.booksService.getBook(this.id)
            .subscribe({
              next: (book) => {
                this.book = book;
              }
            })
        }
      }
    })

  }
  hasAvailableInstances(): boolean {
    if (!this.book.book_instances || this.book.book_instances.length === 0) {
      return false;
    }
    return this.book.book_instances.some((instance: { status: { status_id: number; }; }) => instance.status.status_id === 1);
  }
}
