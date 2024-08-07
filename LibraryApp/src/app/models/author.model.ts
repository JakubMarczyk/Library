import { Book } from "./book.model";

export interface Author{
    author_id?: string;
    firstName: string;
    lastName?: string;
    books?: Book[];
}
