import { AxiosResponse } from "axios";
import {LoginRequest, LoginResult, RegisterRequest} from "../types";
import $api from "../../../api/axios";


export default class AuthService {
    static async login(request: LoginRequest) : Promise<AxiosResponse<LoginResult>>{
        return $api.post<LoginResult>('login', request);
    }

    static async register(request: RegisterRequest) : Promise<AxiosResponse<LoginResult>>{
        return $api.post<LoginResult>('login', request);
    }
    static async logout() : Promise<void>{
        return $api.post('logout');
    }
}