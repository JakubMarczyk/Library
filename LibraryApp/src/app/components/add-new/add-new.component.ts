import { Component, OnInit } from '@angular/core';
import { Author } from 'src/app/models/author.model';
import { Book } from 'src/app/models/book.model';
import { Category } from 'src/app/models/category.model';
import { AuthorsService } from 'src/app/services/authors.service';
import { BooksService } from 'src/app/services/books.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { Bookshelf } from '../../models/bookshelf.model';
import { BookshelvesService } from '../../services/bookshelves.service';

@Component({
  selector: 'app-add-new',
  templateUrl: './add-new.component.html',
  styleUrls: ['./add-new.component.css']
})
export class AddNewComponent implements OnInit{

  authors: Author[] = [];
  selectedAuthors: Author[] = [];

  newAuthor: Author ={
    firstName: ''
  };

  categories: Category[] = [];
  selectedCategories: Category[] = [] 
 
  newCategory: Category ={
    name: ''
  };

  book: Book = {
    title: '',
    isbn: '',
    publisher: '',
    yearOfPublication: undefined,
    cover: '',
    description: '',
    pages: undefined,
    authors: this.selectedAuthors,
    categories: this.selectedCategories
  };

  bookshelf: Bookshelf = {
    name: '',
    floor: 0
  }

  constructor(private booksService: BooksService,
    private authorsService: AuthorsService,
    private categoriesService: CategoriesService,
    private bookshelvesService: BookshelvesService) { }

  ngOnInit(): void {
    this.getAuthors();
    this.getCategories();

  }

  addBook() {
    this.book.authors = this.selectedAuthors;
    this.book.categories = this.selectedCategories;
    if(this.book.title != '' && this.book.authors.length != 0 && this.book.categories.length != 0){
    this.booksService.addBook(this.book).subscribe({
      next: () => {
        this.book = {
          title: '',
          isbn: '',
          publisher: '',
          yearOfPublication: undefined,
          cover: '',
          description: '',
          pages: undefined,
          authors: this.selectedAuthors,
          categories: this.selectedCategories
        };
      },
      error: (response => {
        console.log(response);
      })
    });
  }
  this.reloadPage();
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

  addAuthor(){
    if(this.newAuthor.firstName != '') {
    this.authorsService.addAuthor(this.newAuthor).subscribe({
      next: () => {
        this.newAuthor ={
          firstName: ''
        };
      },
      error: (response => {
        console.log(response);
      })
    });
  }
  this.reloadPage();
  }

  getCategories() {
    this.categoriesService.getCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: (response) =>{
        console.log(response);
      }
    })
  }

  addCategory(){
    if(this.newCategory.name != '') {
    this.categoriesService.addCategory(this.newCategory).subscribe({
      next: () => {
        this.newCategory ={
          name: ''
        };
      },
      error: (response => {
        console.log(response);
      })
    });
  }
  this.reloadPage();
  }

  addBookshelf() {
    if (this.bookshelf.name != '') {
      this.bookshelvesService.addBookshelf(this.bookshelf).subscribe({
        next: () => {
          this.bookshelf = {
            name: '',
            floor: 0,
          }
        },
        error: (response => {
          console.log(response);
        })
      });
    }
  }

  reloadPage(){
    window.location.reload()
  }
}
