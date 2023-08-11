import { User } from "./User";
import { TokenPair } from "./TokenPair";

export interface LoginResult {
    tokenPair: TokenPair;
    user: User;
}