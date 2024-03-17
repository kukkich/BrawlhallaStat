import {StatisticWithFilter} from "../types";

export interface StatisticState {
    statistics: StatisticWithFilter[],
    errors: string[]
}