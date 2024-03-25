import {StatisticState} from "./state";
import {createSlice} from "@reduxjs/toolkit";

export const emptyForm = {
    data: {
        gameType: null,
        legendId: null,
        weaponId: null,
        enemyLegendId: null,
        enemyWeaponId: null,
        teammateLegendId: null,
        teammateWeaponId: null,
    },
    isFetching: false,
    errors: []
}

const initialState: StatisticState = {
    statistics: [],
    form: emptyForm,
    errors: []
}

export const statisticSlice = createSlice({
    name: 'statistic',
    initialState,
    reducers: {
        clearForm(state){
            state.form = emptyForm;
        }
    }
})

export const statisticReducer = statisticSlice.reducer;
export const statisticActions = statisticSlice.actions;