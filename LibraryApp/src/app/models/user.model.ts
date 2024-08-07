import { Borrow } from "./borrow.model";

export interface User {
    user_id?: string;
    nickname: string;
    email: string;
    isAdmin: boolean;
    borrowed?: Borrow[];
}
