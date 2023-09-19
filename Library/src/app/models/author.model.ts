import { Book } from "./book.model";

export interface Author{
    author_id?: number;
    firstName: string;
    lastName?: string;
    books?: Book[];
}