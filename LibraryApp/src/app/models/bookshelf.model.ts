import { Book_Instance } from "./book_instance.model";

export interface Bookshelf {
    bookshelf_id?: string;
    name: string;
    floor: number;
    description?: string;
    book_instances?: Book_Instance[];
}
