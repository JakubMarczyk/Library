import { Book } from 'src/app/models/book.model';
import { Bookshelf } from 'src/app/models/bookshelf.model';
import { Status } from 'src/app/models/status.model';
import { Borrow } from 'src/app/models/borrow.model';

export interface Book_Instance {
  book_instance_id?: string;
  book_id_fk?: string;
  book?: Book;
  bookshelf_id_fk?: string;
  bookshelf?: Bookshelf;
  status_id_fk?: number;
  status?: Status
  borrows?: Borrow[];
}
