import {StatisticFilterCreate, StatisticWithFilter} from "../types";
import {Pagination} from "../types/Pagination";

export interface StatisticFormState {
    data: StatisticFilterCreate,
    isFetching: boolean,
    visible: boolean,
    errors: string[],
}

export interface StatisticState {
    statistics: StatisticWithFilter[],
    pagination: Pagination,
    totalStatistics: number,
    removingFilterId: string | null,
    form: StatisticFormState,
    isFetching: boolean,
    errors: string[]
}