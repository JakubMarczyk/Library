import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/models/book.model';
import { BooksService } from 'src/app/services/books.service';
import { faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { faTrash, faBook } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  books: Book[] = [];
  authorIds: string = "";
  categoryIds: string = "";

  faPen = faPenToSquare;
  faTrash = faTrash;
  faBook = faBook;

  constructor(private booksService: BooksService) {

  }

  ngOnInit(): void {
    this.getBooks();
  }

  getBooks() {
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

  deleteBook(id: string | undefined) {
    if (!id) {
      id = "";
    }
    this.booksService.deleteBook(id).subscribe({
      next: () => {
        this.books = this.books.filter(book => book.book_id !== id);
      },
      error: (response) => {
        console.log(response);
      }
    })
    
  }
}
