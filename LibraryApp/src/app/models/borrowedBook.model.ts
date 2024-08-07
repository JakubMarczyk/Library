import { Author } from 'src/app/models/author.model';
import { Category } from 'src/app/models/category.model';
import { Book_Instance } from 'src/app/models/book_instance.model';

export interface BorrowedBook {
  borrow_id?: string;
  borrowTime: Date;
  returnTime: Date;
  extended: boolean;
    book_instance_fk?: string;
    book_id_fk?: string;
    title: string;
    isbn?: string;
    publisher?: string;
    yearOfPublication?: number;
    cover?: string;
    description?: string;
    pages?: number;
    authors: Author[];
    categories: Category[];
    book_instances?: Book_Instance[];
}
