import { Book } from "./book.model";

export interface Category {
    category_id?: string;
    name: string;
    image?: string;
    books?: Book[];
}
