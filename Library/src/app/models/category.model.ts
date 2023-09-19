import { Book } from "./book.model";

export interface Category {
    category_id?: number;
    name: string;
    image?: string;
    books?: Book[];
}