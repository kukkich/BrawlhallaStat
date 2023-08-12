import AuthService from "../services/AuthService";
import {AppDispatch} from "../../../store";
import {userActions, userSlice} from "./reducer";
import {LoginRequest, RegisterRequest} from "../types";

export const loginAction = (request: LoginRequest) => async (dispatch: AppDispatch) => {
    try {
        dispatch(userActions.loginStart())
        const response = await AuthService.login(request)
        dispatch(userActions.loginSuccess(response.data));
    } catch (e: any) {
        dispatch(userActions.loginFailed(e.message))
    }
}

export const registerAction = (request: RegisterRequest) => async (dispatch: AppDispatch) => {
    try {
        dispatch(userActions.loginStart())
        const response = await AuthService.register(request)
        dispatch(userActions.loginSuccess(response.data));
    } catch (e: any) {
        dispatch(userActions.loginFailed(e.message))
    }
}

export const logoutAction = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(userActions.logoutStart())
        await AuthService.logout()
        dispatch(userActions.logoutSuccess());
    } catch (e: any) {
        dispatch(userActions.logoutFailed(e.message))
        throw new Error("logout failed", e);
    }
}
