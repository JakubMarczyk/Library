import { Borrow } from "./borrow.model";

export interface User {
    user_id?: number;
    nickname: string;
    email: string;
    avatar: string;
    isAdmin: boolean;
    notifications: boolean;
    password?: string;
    borrowed?: Borrow[];
}