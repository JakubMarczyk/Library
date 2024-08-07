import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScanResoultsComponent } from './scan-resoults.component';

describe('ScanResoultsComponent', () => {
  let component: ScanResoultsComponent;
  let fixture: ComponentFixture<ScanResoultsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ScanResoultsComponent]
    });
    fixture = TestBed.createComponent(ScanResoultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
