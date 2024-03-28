import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {BrawlhallaEntitiesState} from "./state";
import {GetEntitiesResult} from "../services/getEntitiesResult";
import {ErrorResponse} from "../../../api/types/ErrorResponse";

const initialState: BrawlhallaEntitiesState = {
    legends: null,
    weapons: null,
    isFetching: false,
    errors: []
}

export const entitiesSlice = createSlice({
    name: 'entities',
    initialState,
    reducers: {
        getEntitiesStart(state: BrawlhallaEntitiesState){
            state.errors = []
            state.isFetching = true
        },
        getEntitiesSuccess(state: BrawlhallaEntitiesState, action: PayloadAction<GetEntitiesResult>){
            state.legends = action.payload.legends
            state.weapons = action.payload.weapons
            state.isFetching = false
        },
        getEntitiesFailed(state: BrawlhallaEntitiesState, action: PayloadAction<ErrorResponse>){
            action.payload.errors.forEach(x => {
                state.errors.push(x);
            })
            state.isFetching = false
        },
    }
})


export const entitiesReducer = entitiesSlice.reducer;
export const entitiesActions = entitiesSlice.actions;

