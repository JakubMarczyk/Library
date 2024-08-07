import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowedComponent } from './borrowed.component';

describe('BorrowedComponent', () => {
  let component: BorrowedComponent;
  let fixture: ComponentFixture<BorrowedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BorrowedComponent]
    });
    fixture = TestBed.createComponent(BorrowedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
