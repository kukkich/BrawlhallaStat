import {AxiosResponse} from "axios";
import $api from "../../../../api/axios";
import {getDetailsFromApiException} from "../../../../api/tools/getDetailsFromApiException";

export default class UserService {
    static async updateProfile(request: UpdateProfileRequest, userId: string): Promise<AxiosResponse> {
        try {
            return await $api.patch<any>(`/users/${userId}`, request);
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }
}

export interface UpdateProfileRequest {
    nickName: string,
    email: string
}