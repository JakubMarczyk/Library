import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookshelvesComponent } from './bookshelves.component';

describe('BookshelfsComponent', () => {
  let component: BookshelvesComponent;
  let fixture: ComponentFixture<BookshelvesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookshelvesComponent]
    });
    fixture = TestBed.createComponent(BookshelvesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
