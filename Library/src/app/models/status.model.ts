import { Book_Instance } from "./book_instance.model";

export interface Status {
    status_id?: number;
    name: string;
    book_instances?: Book_Instance[];
}