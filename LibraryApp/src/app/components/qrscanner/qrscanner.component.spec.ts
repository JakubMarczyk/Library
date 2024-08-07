import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QRscannerComponent } from './qrscanner.component';

describe('QRscannerComponent', () => {
  let component: QRscannerComponent;
  let fixture: ComponentFixture<QRscannerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [QRscannerComponent]
    });
    fixture = TestBed.createComponent(QRscannerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
