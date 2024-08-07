import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Author } from 'src/app/models/author.model';
import { Book } from 'src/app/models/book.model';
import { Category } from 'src/app/models/category.model';
import { AuthorsService } from 'src/app/services/authors.service';
import { BooksService } from 'src/app/services/books.service';
import { CategoriesService } from 'src/app/services/categories.service';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css']
})
export class EditBookComponent {

  id: string | null = "";

  authors: Author[] = [];
  selectedAuthors: Author[] = [];

  categories: Category[] = [];
  selectedCategories: Category[] = [];

  book: Book = {
    title: '',
    isbn: '',
    publisher: '',
    yearOfPublication: 0,
    cover: undefined,
    description: '',
    pages: 0,
    authors: this.selectedAuthors,
    categories: this.selectedCategories
  }

  constructor(private route: ActivatedRoute,
    private router:Router,
    private booksService: BooksService,
    private authorsService: AuthorsService,
    private categoriesService: CategoriesService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      if (params.get('id')) {
        this.id = params.get('id');
        if (this.id) {
          this.booksService.getBook(this.id)
            .subscribe({
              next: (book) => {
                this.book = book;
                this.selectedAuthors = this.book.authors;
                this.selectedCategories = this.book.categories;
              }
            })
        }
      }
    })
    this.getAuthors();
    this.getCategories();

  }

  editBook() {
    this.book.authors = this.selectedAuthors;
    this.book.categories = this.selectedCategories;
    this.booksService.editBook(this.book).subscribe({
      next: () => {
        this.router.navigate(['/books']);
      },
      error: (response => {
        console.log(response);
      })
    });
  }

  getAuthors() {
    this.authorsService.getAuthors().subscribe({
      next: (authors) => {
        this.authors=authors;
      },
      error: (response) =>{
        console.log(response);
      }
    })
  }

  getCategories() {
    this.categoriesService.getCategories().subscribe({
      next: (categories) => {
        this.categories=categories;
      },
      error: (response) =>{
        console.log(response);
      }
    })
  }
}
