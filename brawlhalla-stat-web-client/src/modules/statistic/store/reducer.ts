import {StatisticState} from "./state";
import {createSlice} from "@reduxjs/toolkit";

const initialState: StatisticState = {
    statistics: [],
    errors: []
}

export const statisticSlice = createSlice({
    name: 'statistic',
    initialState,
    reducers: {

    }
})


export const statisticReducer = statisticSlice.reducer;
export const statisticActions = statisticSlice.actions;