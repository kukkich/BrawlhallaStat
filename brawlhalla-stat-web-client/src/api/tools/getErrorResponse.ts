import {ErrorResponse} from "../types/ErrorResponse";

export const getErrorResponse = (e: any): ErrorResponse => {
    if (e instanceof Error) {
        return {errors: [e.message]}
    }
    return e as ErrorResponse;
}