import {AxiosResponse} from "axios";
import $api from "../../../api/axios";
import {StatisticFilterCreate, StatisticWithFilter} from "../types";

export default class StatisticService {
    static async createFilter(request: StatisticFilterCreate): Promise<AxiosResponse<StatisticWithFilter>> {
        return await $api.post<any>('/auth/login', request);
    }
}