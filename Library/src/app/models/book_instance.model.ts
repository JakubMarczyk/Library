import { Book } from 'src/app/models/book.model';
import { Spot } from 'src/app/models/spot.model';
import { Status } from 'src/app/models/status.model';
import { Borrow } from 'src/app/models/borrow.model';

export interface Book_Instance {
    book_instance_id?: number;
    book_id_fk?: number;
    book?: Book;
    spot_id_fk?: number;
    spot?: Spot;
    status_id_fk?: number;
    status?: Status
    borrows?: Borrow[];
}