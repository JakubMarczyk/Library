import { TestBed } from '@angular/core/testing';

import { BookInstancesService } from './book-instances.service';

describe('BookInstancesService', () => {
  let service: BookInstancesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BookInstancesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
