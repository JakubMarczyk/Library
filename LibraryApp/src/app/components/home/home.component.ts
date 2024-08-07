import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/models/book.model';
import { BooksService } from 'src/app/services/books.service';
import { BorrowsService } from '../../services/borrows.service';
import { UsersService } from '../../services/users.service';
import { BorrowedBook } from '../../models/borrowedBook.model';
import { Author } from '../../models/author.model';
import { Category } from '../../models/category.model';
import { CategoriesService } from '../../services/categories.service';
import { AuthorsService } from '../../services/authors.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  books: Book[] = [];
  borrowedBooks: BorrowedBook[] = [];
  defaultCover: string = "https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781787550360/classic-book-cover-foiled-journal-9781787550360_hr.jpg";
  isLogged: boolean = false;
  userId: string = "";

  authors: Author[] = [];
  selectedAuthors: Author[] = [];

  newAuthor: Author = {
    firstName: ''
  };

  categories: Category[] = [];
  selectedCategories: Category[] = []

  newCategory: Category = {
    name: ''
  };

  constructor(private booksService: BooksService, private borrowsService: BorrowsService, private usersService: UsersService, private authorsService: AuthorsService,
    private categoriesService: CategoriesService) {

  }

  ngOnInit(): void {
    this.getBooks();
    this.isUserLogged();
    this.getAuthors();
    this.getCategories();
  }

  getBooks() {
    const authorIds = this.selectedAuthors.map(author => author.author_id).join(',');
    const categoryIds = this.selectedCategories.map(category => category.category_id).join(',');

    const queryParams = {
      authorIds: authorIds,
      categoryIds: categoryIds
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

    isUserLogged() {
      this.isLogged = this.usersService.isLoggedInUser();
      if (this.isLogged) {
        this.userId = this.usersService.getUserId()
        this.getBorrowedBooks();
      }
    }

    getBorrowedBooks() {
      this.borrowsService.getUserBorrowed(this.userId).subscribe({
        next: (borrowedBooks) => {
          this.borrowedBooks = borrowedBooks;
        },
        error: (response) => {
          console.log(response);
        }
      });
  }

  getAuthors() {
    this.authorsService.getAuthors().subscribe({
      next: (authors) => {
        this.authors = authors;
      },
      error: (response) => {
        console.log(response);
      }
    })
  }

  getCategories() {
    this.categoriesService.getCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: (response) => {
        console.log(response);
      }
    })
  }

  applyFilters() {
    this.getBooks();
  }
}
