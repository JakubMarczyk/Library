import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BooksService } from '../../services/books.service';
import { BookshelvesService } from '../../services/bookshelves.service';
import { Book } from '../../models/book.model';
import { Bookshelf } from '../../models/bookshelf.model';
import { CategoriesService } from '../../services/categories.service';

@Component({
  selector: 'app-scan-resoults',
  templateUrl: './scan-resoults.component.html',
  styleUrls: ['./scan-resoults.component.css']
})
export class ScanResoultsComponent implements OnInit {
  type: string = '';
  id: string = '';
  books: Book[] = [];
  bookshelf: Bookshelf | undefined;
  bookshelfName: string = "";
  categoryName: string = "";
  authorIds: string = "";
  categoryIds: string = "";
  defaultCover: string = "https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781787550360/classic-book-cover-foiled-journal-9781787550360_hr.jpg";
  constructor(
    private route: ActivatedRoute,
    private booksService: BooksService,
    private bookshelvesService: BookshelvesService,
    private categoriesService: CategoriesService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.type = params['type'];
      this.id = params['id'];
      this.getBooks();
      this.getNames();
    });
  }

  getBooks() {
    if (this.type === 'bookshelf') {
      this.booksService.getBooksByBookshelf(this.id).subscribe({
        next: (books) => {
          this.books = books;
        },
        error: (response) => {
          console.log(response);
        }
      });

    } else if (this.type === 'category') {
      this.categoryIds = this.id;
      const queryParams = {
        authorIds: this.authorIds,
        categoryIds: this.categoryIds
      };
      this.booksService.getBooks(queryParams).subscribe({
        next: (books) => {
          this.books = books;
        },
        error: (response) => {
          console.log(response);
        }
      });
    }
  }

  getNames() {
    if (this.type === 'bookshelf') {
      this.bookshelvesService.getBookshelf(this.id).subscribe({
        next: (bookshelf) => {
          this.bookshelfName = bookshelf.name;
        },
        error: (response) => {
          console.log(response)
        }
      });
    } else if (this.type === 'category') {
      this.categoriesService.getCategory(this.id).subscribe({
        next: (category) => {
          this.categoryName = category.name;
        },
        error: (response) => {
          console.log(response)
        }
      });

    }
  }
}
