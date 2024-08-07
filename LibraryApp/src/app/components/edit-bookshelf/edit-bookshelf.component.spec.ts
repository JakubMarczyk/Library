import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBookshelfComponent } from './edit-bookshelf.component';

describe('EditBookshelfComponent', () => {
  let component: EditBookshelfComponent;
  let fixture: ComponentFixture<EditBookshelfComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditBookshelfComponent]
    });
    fixture = TestBed.createComponent(EditBookshelfComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
