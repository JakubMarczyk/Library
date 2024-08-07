import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { BooksService } from 'src/app/services/books.service';
import { Bookshelf } from 'src/app/models/bookshelf.model';
import { Status } from '../../models/status.model';
import { StatusesService } from '../../services/statuses.service';
import { BookshelvesService } from '../../services/bookshelves.service';
import { BookInstancesService } from '../../services/book-instances.service';
import { Edit_Book_Instance } from '../../models/edit_book_instance.model';

@Component({
  selector: 'app-book-instances',
  templateUrl: './book-instances.component.html',
  styleUrls: ['./book-instances.component.css']
})
export class BookInstancesComponent implements OnInit {
  id: string | null = "";
  book: any;
  bookshelves: Bookshelf[] = [];
  statuses: Status[] = [];
  editInstance: Edit_Book_Instance | undefined;
  addInstance: Edit_Book_Instance | undefined;
  selectedBookshelf: Bookshelf[] = [];
  selectedStatus: Status[] = [];
  addStatus: Status = {
    status_id: 1,
    name: "Dostępna"
  };
  addBookshelf: Bookshelf | undefined;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private booksService: BooksService,
    private bookshelvesService: BookshelvesService,
    private statusesService: StatusesService,
    private bookInstanceService: BookInstancesService) { }

  ngOnInit(): void {
    this.getBookInstances();
    this.getBookshelves();
    this.getStatuses();
  }

  getBookInstances() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      if (params.get('id')) {
        this.id = params.get('id');
        if (this.id) {
          this.booksService.getBook(this.id)
            .subscribe({
              next: (book) => {
                this.book = book;
                this.initSelectedValues();
              }
            })
        }
      }
    })
  }

  getBookshelves() {
    this.bookshelvesService.getBookshelves().subscribe({
      next: (bookshelves) => {
        this.bookshelves = bookshelves;
      },
      error: (response) => {
        console.log(response);
      }
    })
  }

  initSelectedValues() {
    if (this.book && this.book.book_instances) {
      this.book.book_instances.forEach((instance: any, index: number) => {
        this.selectedBookshelf[index] = instance.bookshelf;
        this.selectedStatus[index] = instance.status;
      });
    }
  }

  getStatuses() {
    this.statusesService.getStatuses().subscribe({
      next: (statuses) => {
        this.statuses = statuses;
      },
      error: (response) => {
        console.log(response);
      }
    })
  }

  delBookInstance(instance: any) {
    this.bookInstanceService.deleteBookInstance(instance.book_instance_id)
      .subscribe({
        next: (response) => {
          this.getBookInstances();
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
  }

  addBookInstance() {
    this.addInstance = {
      book_id_fk: this.book.book_id,
      bookshelf_id_fk: this.addBookshelf?.bookshelf_id,
      status_id_fk: this.addStatus.status_id
    }
    this.bookInstanceService.createBookInstance(this.addInstance)
      .subscribe({
        next: (response) => {
          this.getBookInstances();
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
  }

  updateBookInstance(instance: any, i: number) {
    this.editInstance = {
      bookshelf_id_fk: this.selectedBookshelf[i].bookshelf_id,
      status_id_fk: this.selectedStatus[i].status_id
    }
    this.bookInstanceService.updateBookInstance(instance.book_instance_id, this.editInstance)
      .subscribe({
        next: (response) => {

        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
  }
}
