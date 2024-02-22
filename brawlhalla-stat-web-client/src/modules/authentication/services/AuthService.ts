import axios, {AxiosResponse} from "axios";
import {LoginRequest, LoginResult, RegisterRequest} from "../types";
import $api, {API_URL} from "../../../api/axios";

export default class AuthService {
    static async login(request: LoginRequest): Promise<AxiosResponse<LoginResult>> {
        let result = $api.post<any>('/auth/login', request);
        console.log(result);
        return result;
    }

    static async register(request: RegisterRequest): Promise<AxiosResponse<LoginResult>> {
        return $api.post<LoginResult>('/auth/register', request);
    }

    static async logout(): Promise<void> {
        return $api.post('/auth/logout');
    }

    static async refresh(): Promise<AxiosResponse<LoginResult>> {
        return axios.get<LoginResult>(`${API_URL}/auth/refresh`, {withCredentials: true})
    }
}