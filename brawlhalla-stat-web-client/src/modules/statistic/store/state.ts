import {StatisticFilterCreate, StatisticWithFilter} from "../types";

export interface StatisticFormState {
    data: StatisticFilterCreate,
    isFetching: boolean,
    errors: string[]
}

export interface StatisticState {
    statistics: StatisticWithFilter[],
    form: StatisticFormState,
    errors: string[]
}