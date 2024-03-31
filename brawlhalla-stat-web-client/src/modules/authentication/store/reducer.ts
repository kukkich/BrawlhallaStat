import {LoginStatus, UserState} from "./state";
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
            localStorage.setItem('token', action.payload.accessToken)
            state.status = LoginStatus.authorized;
            state.user = action.payload.user;
        },
        loginFailed(state, action: PayloadAction<string[]>){
            state.status = LoginStatus.unauthorized
            console.log(action.payload)
            action.payload.forEach(x => {
                state.errors.push(x);
            })
        },

        logoutStart(state){
            state.status = LoginStatus.logouting
        },
        logoutFinished(state){
            localStorage.removeItem('token')
            state.status = LoginStatus.unauthorized
            state.user = null
        },

        checkAuthStart(state){
            state.status = LoginStatus.authChecking
        },
        checkAuthFailed(state){
            state.status = LoginStatus.unauthorized
        },
    }
})


export const userReducer = userSlice.reducer;
export const userActions = userSlice.actions;