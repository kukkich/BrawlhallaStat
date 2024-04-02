import $api from "../../../api/axios";
import {StatisticFilterCreate, StatisticWithFilter} from "../types";
import {getDetailsFromApiException} from "../../../api/tools/getDetailsFromApiException";
import {PagedStatisticWithFilter} from "../types/filters";

export default class StatisticService {
    static async getStatistics(): Promise<StatisticWithFilter[]> {
        try {
            const response = await $api.get<StatisticWithFilter[]>('/statistic/userStatistics');
            return response.data;
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }

    static async getStatisticsPaged(number: number, size: number): Promise<PagedStatisticWithFilter> {
        try {
            const response = await $api.get<PagedStatisticWithFilter>(`/statistic/userStatistics?number=${number}&size=${size}`);
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
        try {
            await $api.delete(`/statistic/filters/${id}`);
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }
}