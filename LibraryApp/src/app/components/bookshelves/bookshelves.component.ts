import { Component, OnInit } from '@angular/core';
import { Bookshelf } from 'src/app/models/bookshelf.model';
import { BookshelvesService } from 'src/app/services/bookshelves.service';
import { faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-bookshelves',
  templateUrl: './bookshelves.component.html',
  styleUrls: ['./bookshelves.component.css']
})
export class BookshelvesComponent {
  bookshelves: Bookshelf[] = [];
  faPen = faPenToSquare;
  faTrash = faTrash;
  constructor(private bookshelvesService: BookshelvesService) { }

  ngOnInit(): void {
    this.getBookshelfs();
  }

  getBookshelfs() {
    this.bookshelvesService.getBookshelves().subscribe({
      next: (bookshelves) => {
        this.bookshelves = bookshelves;
      },
      error: (response) => {
        console.log(response);
      }
    })
  }

  deleteBookshelf(id: string | undefined) {
    if (!id) {
      id = "";
    }
    this.bookshelvesService.deleteBookshelf(id).subscribe({
      next: () => {
        this.bookshelves = this.bookshelves.filter(bookshelf => bookshelf.bookshelf_id !== id);
      },
      error: (response) => {
        console.log(response);
      }
    })
  }
}
