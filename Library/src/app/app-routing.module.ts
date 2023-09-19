import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksComponent } from './components/books/books.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { AuthorsComponent } from './components/authors/authors.component';
import { EditBookComponent } from './components/edit-book/edit-book.component';
import { AddNewComponent } from './components/add-new/add-new.component';
import { EditAuthorComponent } from './components/edit-author/edit-author.component';
import { EditCategoryComponent } from './components/edit-category/edit-category.component';
import { HomeComponent } from './components/home/home.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },

  {
    path: 'books',
    component: BooksComponent
  },

  {
    path: 'addNew',
    component: AddNewComponent
  },

  {
    path: 'books/edit/:id',
    component: EditBookComponent
  },

  {
    path: 'authors',
    component: AuthorsComponent
  },

  {
    path: 'authors/edit/:id',
    component: EditAuthorComponent
  },

  {
    path: 'categories',
    component: CategoriesComponent
  },

  {
    path: 'categories/edit/:id',
    component: EditCategoryComponent
  },

  {path: '**',
  component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
