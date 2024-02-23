import { User } from "./User";

export interface LoginResult {
    accessToken: string;
    user: User;
}