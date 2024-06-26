import {StatisticState} from "./state";
import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {StatisticFilterCreate, StatisticWithFilter} from "../types";
import {PagedStatisticWithFilter} from "../types/filters";
import {Pagination} from "../types/Pagination";

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
    visible: false,
    errors: []
}

const initialState: StatisticState = {
    statistics: [],
    pagination: {
        page: 0,
        pageSize: 5,
    },
    totalStatistics: 0,
    removingFilterId: null,
    form: emptyForm,
    isFetching: false,
    errors: []
}

export const statisticSlice = createSlice({
    name: 'statistic',
    initialState,
    reducers: {
        openForm(state) {
            state.form.visible = true
        },
        closeForm(state) {
            state.form = {
                ...emptyForm,
                visible: state.form.visible
            }
            state.form.visible = false
        },
        setFormState(state, action: PayloadAction<StatisticFilterCreate>) {
            state.form.data = action.payload;
        },
        clearForm(state) {
            state.form = {
                ...emptyForm,
                visible: state.form.visible
            }
        },
        submitFormStart(state) {
            state.form.isFetching = true;
            state.form.errors = [];
        },
        submitFormSuccess(state) {
            state.form = emptyForm;
        },
        submitFormFailed(state, action: PayloadAction<string[]>) {
            state.form.isFetching = false;
            state.form.errors = action.payload;
        },

        fetchStatisticsStart(state) {
            state.isFetching = true;
            state.errors = [];
        },
        fetchStatisticsSuccess(state, action: PayloadAction<StatisticWithFilter[]>) {
            state.isFetching = false;
            state.statistics = action.payload;
        },
        fetchStatisticsFailed(state, action: PayloadAction<string[]>) {
            state.isFetching = false;

            action.payload.forEach(x => {
                state.errors.push(x);
            })
        },

        fetchPagedStatisticsSuccess(state, action: PayloadAction<PagedStatisticWithFilter>) {
            state.isFetching = false;
            state.statistics = action.payload.statisticWithFilter;
            state.totalStatistics = action.payload.total;
        },
        setPagination(state, action: PayloadAction<Pagination>) {
            state.pagination = action.payload;
        },

        deleteFilterStart(state, action: PayloadAction<string>) {
            state.removingFilterId = action.payload;
        },
        deleteFilterSuccess(state, action: PayloadAction<string>) {
            state.removingFilterId = null;
            // state.statistics = state.statistics.filter(x => x.filter.id !== action.payload)
        },
        deleteFilterFailed(state, action: PayloadAction<string[]>) {
            state.removingFilterId = null;
            action.payload.forEach(x => {
                state.errors.push(x);
            })
        },
    }
})

export const statisticReducer = statisticSlice.reducer;
export const statisticActions = statisticSlice.actions;