import { TestBed } from '@angular/core/testing';

import { BookshelvesService } from './bookshelves.service';

describe('BookshelvesService', () => {
  let service: BookshelvesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BookshelvesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
