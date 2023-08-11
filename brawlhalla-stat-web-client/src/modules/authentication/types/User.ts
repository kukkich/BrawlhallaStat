import { Claim } from "./Claim";
import { Role } from "./Role";

export interface User {
    id: string;
    login: string;
    nickName: string;
    email: string;
    roles: Role[];
    claims: Claim[];
}