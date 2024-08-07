import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgSelectModule } from "@ng-select/ng-select";
import { QRCodeModule } from 'angularx-qrcode';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BooksComponent } from './components/books/books.component';
import { AuthorsComponent } from './components/authors/authors.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { EditBookComponent } from './components/edit-book/edit-book.component';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AddNewComponent } from './components/add-new/add-new.component';
import { EditCategoryComponent } from './components/edit-category/edit-category.component';
import { EditAuthorComponent } from './components/edit-author/edit-author.component';
import { HomeComponent } from './components/home/home.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { BookComponent } from './components/book/book.component';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { LogoutPageComponent } from './components/logout-page/logout-page.component';
import { ProfileComponent } from './components/profile/profile.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { JwtInterceptor } from './interceptors/JwtInterceptor';
import { BookshelvesComponent } from './components/bookshelves/bookshelves.component';
import { EditBookshelfComponent } from './components/edit-bookshelf/edit-bookshelf.component';
import { BorrowedComponent } from './components/borrowed/borrowed.component';
import { QRscannerComponent } from './components/qrscanner/qrscanner.component';
import { ZXingScannerModule } from '@zxing/ngx-scanner';
import { BorrowedBookComponent } from './components/borrowed-book/borrowed-book.component';
import { BookInstancesComponent } from './components/book-instances/book-instances.component';
import { ScanResoultsComponent } from './components/scan-resoults/scan-resoults.component';
import { ChatbotComponent } from './components/chatbot/chatbot.component';

@NgModule({
  declarations: [
    AppComponent,
    BooksComponent,
    AuthorsComponent,
    CategoriesComponent,
    EditBookComponent,
    AddNewComponent,
    EditCategoryComponent,
    EditAuthorComponent,
    HomeComponent,
    PageNotFoundComponent,
    BookComponent,
    LoginPageComponent,
    RegisterPageComponent,
    LogoutPageComponent,
    ProfileComponent,
    NavbarComponent,
    BookshelvesComponent,
    EditBookshelfComponent,
    BorrowedComponent,
    QRscannerComponent,
    BorrowedBookComponent,
    BookInstancesComponent,
    ScanResoultsComponent,
    ChatbotComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgSelectModule,
    FontAwesomeModule,
    ZXingScannerModule,
    QRCodeModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
