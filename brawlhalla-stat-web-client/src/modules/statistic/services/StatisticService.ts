import {AxiosResponse} from "axios";
import $api from "../../../api/axios";
import {LegendStatistic} from "../types/LegendStatistic";

export default class StatisticService {
    static async getLegendStat(): Promise<AxiosResponse<LegendStatistic[]>> {
        return $api.get<LegendStatistic[]>('/legends');
    }
}