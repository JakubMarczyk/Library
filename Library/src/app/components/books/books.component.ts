import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/models/book.model';
import { BooksService } from 'src/app/services/books.service';
import { faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  books: Book[] = [];

  faPen = faPenToSquare;
  faTrash = faTrash;

  constructor(private booksService: BooksService) {

  }

  ngOnInit(): void {
    this.getBooks();
  }

  getBooks() {
    this.booksService.getBooks().subscribe({
      next: (books) => {
        this.books = books;
        this.books.sort((a, b) => Number(a.book_id) - Number(b.book_id));
      },
      error: (response) => {
        console.log(response);
      }
    });
  }

  deleteBook(id: number | undefined) {
    id = Number(id);
    this.booksService.deleteBook(id).subscribe({
      next: () => {
        this.books.splice(this.books.findIndex(book => book.book_id === id, 1));
      },
      error: (response) => {
        console.log(response);
      }
    })
    
  }
}
