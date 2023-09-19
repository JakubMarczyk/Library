import { Book_Instance } from 'src/app/models/book_instance.model';
import { User } from 'src/app/models/user.model';

export interface Borrow {
    borrow_id?: number;
    book_instance_id_fk: number;
    book_instance: Book_Instance;
    user_id_fk: number;
    user: User;
    borrowTime: Date ;
    returnTime: Date;
    returnedTime?: Date;
}