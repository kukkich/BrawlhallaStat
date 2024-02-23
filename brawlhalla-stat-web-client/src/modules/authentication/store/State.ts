import {User} from "../types";

export enum LoginStatus {
    unauthorized,
    loginning,
    authorized,
    logouting,
    authChecking
}

export interface UserState {
    user: User | null,
    status: LoginStatus,
    errors: string[]
}