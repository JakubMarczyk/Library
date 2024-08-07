import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService } from 'src/app/services/users.service';
import { BooksService } from '../../services/books.service';
import { Book } from '../../models/book.model';
import { faHome, faQrcode, faUser, faComment, faList } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  constructor(private usersService: UsersService, private booksService: BooksService, private router: Router) { }
  books: Book[] = [];
  searchedText: string = "";
  defaultCover: string = "https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781787550360/classic-book-cover-foiled-journal-9781787550360_hr.jpg";
  isSearchFocused: boolean = false;

  faHome = faHome;
  faQrcode = faQrcode;
  faChat = faComment;
  faUser = faUser;
  faList = faList;

  @HostListener('document:click', ['$event'])
  onClick(event: Event) {
    const target = event.target as HTMLElement;
    if (!target.closest('.search-dropdown') && !target.closest('.form-control')) {
      this.isSearchFocused = false;
    }
  }

  logout() {
    this.usersService.logout();
  }

  isUserLogged(): boolean {
    return this.usersService.isLoggedInUser();
  }

  getRole(): string {
    return this.usersService.getUserRole();
  }

  searchBooks() {
    this.isSearchFocused = true;
    if (this.searchedText != "") {
      this.booksService.search(this.searchedText).subscribe({
        next: (books) => {
          this.books = books;
        },
        error: (response) => {
          console.log(response);
        }
      })
    } else
      this.books = [];
  }


}
