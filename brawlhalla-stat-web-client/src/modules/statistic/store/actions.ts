import {AppDispatch, RootState} from "../../../store";
import StatisticService from "../services/StatisticService";
import {statisticActions} from "./reducer";
import {getErrorResponse} from "../../../api/tools/getErrorResponse";

export const submitForm = () => async (dispatch: AppDispatch, getState: () => RootState) => {
    try {
        dispatch(statisticActions.submitFormStart())

        const state = getState().statisticReducer;
        const data = state.form.data;
        await StatisticService.createFilter(data);

        dispatch(statisticActions.submitFormSuccess());
        await dispatch(fetchPagedStatistics(state.pagination.page, state.pagination.pageSize))
    } catch (e: any){
        dispatch(statisticActions.submitFormFailed(getErrorResponse(e).errors))
    }
}

export const fetchStatistics = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(statisticActions.fetchStatisticsStart())
        const statistics = await StatisticService.getStatistics()
        dispatch(statisticActions.fetchStatisticsSuccess(statistics))
    } catch (e: any){
        dispatch(statisticActions.fetchStatisticsFailed(getErrorResponse(e).errors))
    }
}

export const fetchPagedStatistics = (number: number, size: number) => async (dispatch: AppDispatch) => {
    try {
        // dispatch(statisticActions.setPagination({page: number, pageSize: size}))
        dispatch(statisticActions.fetchStatisticsStart())
        const response = await StatisticService.getStatisticsPaged(number + 1, size)
        dispatch(statisticActions.fetchPagedStatisticsSuccess(response))
    } catch (e: any){
        dispatch(statisticActions.fetchStatisticsFailed(getErrorResponse(e).errors))
    }
}

export const deleteFilter = (id: string) => async (dispatch: AppDispatch, getState: () => RootState) => {
    try {
        dispatch(statisticActions.deleteFilterStart(id))
        await StatisticService.deleteFilter(id);
        dispatch(statisticActions.deleteFilterSuccess(id))
        const state = getState().statisticReducer;

        await dispatch(fetchPagedStatistics(state.pagination.page, state.pagination.pageSize))
    } catch (e: any) {
        dispatch(statisticActions.deleteFilterFailed(getErrorResponse(e).errors))
    }
}