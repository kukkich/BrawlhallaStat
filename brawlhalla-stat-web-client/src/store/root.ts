import { combineReducers } from 'redux';
import {themeReducer} from "../modules/theme";
import {configureStore} from "@reduxjs/toolkit";
import { userReducer } from '../modules/authentication';

export const rootReducer = combineReducers({
    themeReducer,
    userReducer,
});

export const rootStore = configureStore({
    reducer: rootReducer,
});

export const setupStore = () => {
    return configureStore({
        reducer: rootReducer
    })
}

export type RootState = ReturnType<typeof rootReducer>
export type AppStore = ReturnType<typeof setupStore>
export type AppDispatch = AppStore['dispatch']