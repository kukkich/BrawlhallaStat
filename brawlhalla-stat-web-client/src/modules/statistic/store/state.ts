import {StatisticFilterCreate, StatisticWithFilter} from "../types";

export interface StatisticFormState {
    data: StatisticFilterCreate,
    isFetching: boolean,
    errors: string[],
}

export interface StatisticState {
    statistics: StatisticWithFilter[],
    removingFilterId: string | null,
    form: StatisticFormState,
    isFetching: boolean,
    errors: string[]
}