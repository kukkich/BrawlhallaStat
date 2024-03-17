import {LoginRequest, LoginResult} from "../../authentication/types";
import {AxiosResponse} from "axios/index";
import $api from "../../../api/axios";

export default class StatisticService {
    static async login(request: LoginRequest): Promise<AxiosResponse<LoginResult>> {
        return await $api.post<any>('/auth/login', request);
    }
}