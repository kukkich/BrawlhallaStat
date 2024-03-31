import {AppDispatch, RootState} from "../../../store";
import StatisticService from "../services/StatisticService";
import {statisticActions} from "./reducer";
import {getErrorResponse} from "../../../api/tools/getErrorResponse";

export const submitForm = () => async (dispatch: AppDispatch, getState: () => RootState) => {
    try {
        dispatch(statisticActions.submitFormStart())

        const state = getState();
        const data = state.statisticReducer.form.data;
        const response = await StatisticService.createFilter(data);

        dispatch(statisticActions.submitFormSuccess(response));
        await dispatch(fetchStatistics())
    } catch (e: any){
        dispatch(statisticActions.submitFormFailed(getErrorResponse(e).errors))
    }
}

export const fetchStatistics = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(statisticActions.fetchStatisticsStart())
        const statistics = await StatisticService.getFilters()
        dispatch(statisticActions.fetchStatisticsSuccess(statistics))
    } catch (e: any){
        dispatch(statisticActions.fetchStatisticsFailed(getErrorResponse(e).errors))
    }
}

export const deleteFilter = (id: string) => async (dispatch: AppDispatch) => {
    try {
        dispatch(statisticActions.deleteFilterStart(id))
        await StatisticService.deleteFilter(id);
        dispatch(statisticActions.deleteFilterSuccess(id))
    } catch (e: any) {
        dispatch(statisticActions.deleteFilterFailed(getErrorResponse(e).errors))
    }
}