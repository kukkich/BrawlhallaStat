import {User} from "../types";

export enum LoginStatus {
    unauthorized,
    loginning,
    authorized,
    logouting
}

export interface UserState {
    user: User | null,
    status: LoginStatus,
    errors: string[]
}