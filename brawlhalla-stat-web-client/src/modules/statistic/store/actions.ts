import {AppDispatch, RootState} from "../../../store";
import StatisticService from "../services/StatisticService";
import {statisticActions} from "./reducer";

export const submitForm = () => async (dispatch: AppDispatch, getState: () => RootState) => {
    try {
        dispatch(statisticActions.submitFormStart())

        const state = getState();
        const data = state.statisticReducer.form.data;
        const response = await StatisticService.createFilter(data);

        dispatch(statisticActions.submitFormSuccess(response));
        dispatch(fetchStatistics())
    } catch (e: any){
        console.log(e)
        dispatch(statisticActions.submitFormFailed(e))
    }
}

export const fetchStatistics = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(statisticActions.fetchStatisticsStart())
        const statistics = await StatisticService.getFilters()
        dispatch(statisticActions.fetchStatisticsSuccess(statistics))
    } catch (e: any){
        dispatch(statisticActions.fetchStatisticsFailed(e))
    }
}