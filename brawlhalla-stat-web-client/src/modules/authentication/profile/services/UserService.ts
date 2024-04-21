import {AxiosResponse} from "axios";
import $api from "../../../../api/axios";
import {getDetailsFromApiException} from "../../../../api/tools/getDetailsFromApiException";

export default class UserService {
    static async login(request: UpdateProfileRequest): Promise<AxiosResponse> {
        try {
            return await $api.post<any>('/user/profile', request);
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }
}

export interface UpdateProfileRequest {
    nickName: string,
    email: string
}