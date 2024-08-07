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
import { LoginPageComponent } from './components/login-page/login-page.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { LogoutPageComponent } from './components/logout-page/logout-page.component';
import { ProfileComponent } from './components/profile/profile.component';
import { BookComponent } from './components/book/book.component';
import { BookshelvesComponent } from './components/bookshelves/bookshelves.component';
import { EditBookshelfComponent } from './components/edit-bookshelf/edit-bookshelf.component';
import { RoleGuard } from './guards/RoleGuard';
import { QRscannerComponent } from './components/qrscanner/qrscanner.component';
import { BorrowedBookComponent } from './components/borrowed-book/borrowed-book.component';
import { BookInstancesComponent } from './components/book-instances/book-instances.component';
import { ScanResoultsComponent } from './components/scan-resoults/scan-resoults.component';
import { ChatbotComponent } from './components/chatbot/chatbot.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },

  {
    path: 'book/:id',
    component: BookComponent
  },

  {
    path: 'borrowedBook/:id',
    component: BorrowedBookComponent,
  },

  {
    path: 'books',
    component: BooksComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'books/bookInstances/:id',
    component: BookInstancesComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'addNew',
    component: AddNewComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'books/edit/:id',
    component: EditBookComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'authors',
    component: AuthorsComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'authors/edit/:id',
    component: EditAuthorComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'categories',
    component: CategoriesComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'categories/edit/:id',
    component: EditCategoryComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'bookshelves',
    component: BookshelvesComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'bookshelves/edit/:id',
    component: EditBookshelfComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'login',
    component: LoginPageComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'register',
    component: RegisterPageComponent,
    canActivate: [RoleGuard]
  },

  {
    path: 'logout',
    component: LogoutPageComponent
  },

  {
    path: 'profile',
    component: ProfileComponent,
  },

  {
    path: 'QRscanner',
    component: QRscannerComponent,
  },

  {
    path: 'scanInfo/:type/:id',
    component: ScanResoultsComponent,
  },

  {
    path: 'chatBot',
    component: ChatbotComponent
  },

  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
