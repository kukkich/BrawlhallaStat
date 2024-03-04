import {AxiosResponse} from "axios";
import $api from "../../../api/axios";
import {GetEntitiesResult} from "./getEntitiesResult";

export default class entitiesService {
    static async getEntities(): Promise<AxiosResponse<GetEntitiesResult>> {
        throw new Error("Not implemented");
        return $api.post<any>('/auth/login');
    }
}