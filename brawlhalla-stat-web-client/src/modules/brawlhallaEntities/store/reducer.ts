import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {BrawlhallaEntitiesState} from "./state";
import {GetEntitiesResult} from "../services/getEntitiesResult";

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
        getEntitiesFailed(state: BrawlhallaEntitiesState, error: any){
            state.errors.push(error)
            state.isFetching = false
        },
    }
})


export const entitiesReducer = entitiesSlice.reducer;
export const entitiesActions = entitiesSlice.actions;

