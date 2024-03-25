import $api from "../../../api/axios";
import {StatisticFilterCreate, StatisticWithFilter} from "../types";

export default class StatisticService {
    static async createFilter(request: StatisticFilterCreate): Promise<StatisticWithFilter> {
        const response = await $api.post('/statistic/filters', request);
        return response.data;
    }
}