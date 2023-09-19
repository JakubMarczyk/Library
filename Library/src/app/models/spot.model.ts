import { Book_Instance } from "./book_instance.model";

export interface Spot {
    spot_id?: number;
    name: string;
    floor: number;
    description?: string;
    book_instances?: Book_Instance[];
}