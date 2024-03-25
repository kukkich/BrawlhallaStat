import $api from "../../../api/axios";
import {StatisticFilterCreate, StatisticWithFilter} from "../types";

export default class StatisticService {
    static async getFilters(): Promise<StatisticWithFilter[]> {
        const response = await $api.get<StatisticWithFilter[]>('/statistic/userFilters');
        return response.data;
    }

    static async createFilter(request: StatisticFilterCreate): Promise<StatisticWithFilter> {
        const response = await $api.post('/statistic/filters', request);
        return response.data;
    }

    static async deleteFilter(id: string) : Promise<void> {
        await $api.delete('/statistic/filters', { data: { id } });
    }
}