import {AppDispatch} from "../../../store";
import StatisticService from "../services/StatisticService";
import {StatisticFilterCreate} from "../types";
import {statisticActions} from "./reducer";

export const submitForm = (request: StatisticFilterCreate) => async (dispatch: AppDispatch) => {
    try {
        dispatch(statisticActions.submitFormStart())
        const response = await StatisticService.createFilter(request);
        console.log('фильтер добавлен')
        console.log(response)
        dispatch(statisticActions.submitFormSuccess(response));
        dispatch(fetchStatistics())
    } catch (e: any){
        dispatch(statisticActions.submitFormFailed(e))
    }
}

export const fetchStatistics = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(statisticActions.fetchStatisticsStart())
        const statistics = await StatisticService.getFilters()
        dispatch(statisticActions.fetchStatisticsSuccess(statistics))
        console.log(statistics)
    } catch (e: any){
        dispatch(statisticActions.fetchStatisticsFailed(e))
    }
}