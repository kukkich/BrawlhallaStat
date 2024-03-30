import {AxiosError} from "axios";
import {ErrorResponse} from "../types/ErrorResponse";

export const getDetailsFromApiException = (e: any): ErrorResponse => {
    if (!(e instanceof AxiosError)) {
        throw new Error("Unexpected error", {cause: e})
    }
    const error = e as AxiosError
    if (error.response!.status === 404) {
        return {errors: ['Resource not found']}
    }

    // @ts-ignore
    const data = error.response.data as ErrorResponse
    console.log(data)
    return data
}