import {LoginStatus, UserState} from "./State";
import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {LoginResult} from "../types";

const initialState: UserState = {
    user: null,
    status: LoginStatus.unauthorized,
    errors: []
}

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        clearErrors(state){
            state.errors = [];
        },
        loginStart(state){
            state.status = LoginStatus.loginning;
            state.errors = []
        },
        loginSuccess(state, action: PayloadAction<LoginResult>){
            localStorage.setItem('token', action.payload.tokenPair.access)
            state.status = LoginStatus.authorized;
            state.user = action.payload.user;
        },
        loginFailed(state, actions: PayloadAction<string>){
            state.status = LoginStatus.unauthorized
            state.errors.push(actions.payload)
        },

        logoutStart(state){
            state.status = LoginStatus.logouting
        },
        logoutSuccess(state){
            localStorage.removeItem('token')
            state.status = LoginStatus.unauthorized
            state.user = null
        },
        logoutFailed(state){
            state.status = LoginStatus.authorized
        },

        checkAuthStart(state){
            state.status = LoginStatus.authChecking
        },
        checkAuthFailed(state, error: any){
            state.status = LoginStatus.unauthorized
            console.log(error.response?.data?.message);
        },
    }
})


export const userReducer = userSlice.reducer;
export const userActions = userSlice.actions;