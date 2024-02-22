import { User } from "./User";

export interface LoginResult {
    tokenPair: string;
    user: User;
}