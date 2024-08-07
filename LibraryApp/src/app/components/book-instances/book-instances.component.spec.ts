import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookInstancesComponent } from './book-instances.component';

describe('BookInstancesComponent', () => {
  let component: BookInstancesComponent;
  let fixture: ComponentFixture<BookInstancesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookInstancesComponent]
    });
    fixture = TestBed.createComponent(BookInstancesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
