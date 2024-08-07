import { TestBed } from '@angular/core/testing';

import { InfoScannerService } from './info-scanner.service';

describe('InfoScannerService', () => {
  let service: InfoScannerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InfoScannerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
