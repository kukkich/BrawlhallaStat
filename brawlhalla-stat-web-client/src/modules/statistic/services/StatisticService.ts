import $api from "../../../api/axios";
import {StatisticFilterCreate, StatisticWithFilter} from "../types";
import {getDetailsFromApiException} from "../../../api/tools/getDetailsFromApiException";

export default class StatisticService {
    static async getFilters(): Promise<StatisticWithFilter[]> {
        try {
            const response = await $api.get<StatisticWithFilter[]>('/statistic/userFilters');
            return response.data;
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }

    static async createFilter(request: StatisticFilterCreate): Promise<StatisticWithFilter> {
        try {
            const response = await $api.post('/statistic/filters', request);
            return response.data;
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }

    static async deleteFilter(id: string): Promise<void> {
        console.log(`remove by id ${id}`)
        try {
            await $api.delete(`/statistic/filters/${id}`);
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }
}