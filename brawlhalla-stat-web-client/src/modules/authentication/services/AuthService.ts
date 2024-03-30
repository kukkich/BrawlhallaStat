import axios, {AxiosResponse} from "axios";
import {LoginRequest, LoginResult, RegisterRequest} from "../types";
import $api from "../../../api/axios";
import {getDetailsFromApiException} from "../../../api/tools/getDetailsFromApiException";

export default class AuthService {
    static async login(request: LoginRequest): Promise<AxiosResponse<LoginResult>> {
        try {
            return await $api.post<any>('/auth/login', request);
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }

    static async register(request: RegisterRequest): Promise<AxiosResponse<LoginResult>> {
        try {
            return await $api.post<LoginResult>('/auth/register', request);
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }

    static async logout(): Promise<void> {
        try {
            return await $api.post('/auth/logout');
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }

    static async refresh(): Promise<AxiosResponse<LoginResult>> {
        try {
            return await axios.post<LoginResult>('/auth/refresh');
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }
}