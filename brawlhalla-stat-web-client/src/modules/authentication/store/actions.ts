import AuthService from "../services/AuthService";
import {AppDispatch} from "../../../store";
import {userActions} from "./reducer";
import {LoginRequest, LoginResult, RegisterRequest} from "../types";
import {getMessageFromError} from "../../../api/tools/getMessageFromError";
import axios from "axios";
import {API_URL} from "../../../api/axios";

export const loginAction = (request: LoginRequest) => async (dispatch: AppDispatch) => {
    try {
        dispatch(userActions.loginStart())
        const response = await AuthService.login(request)
        dispatch(userActions.loginSuccess(response.data));
    } catch (e: any) {
        dispatch(userActions.loginFailed(getMessageFromError(e)))
    }
}

export const registerAction = (request: RegisterRequest) => async (dispatch: AppDispatch) => {
    try {
        dispatch(userActions.loginStart())
        const response = await AuthService.register(request)
        dispatch(userActions.loginSuccess(response.data));
    } catch (e: any) {
        dispatch(userActions.loginFailed(getMessageFromError(e)))
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

export const checkAuthAction = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(userActions.checkAuthStart())
        const response = await AuthService.refresh();
        dispatch(userActions.loginSuccess(response.data));
    } catch (e: any) {
        dispatch(userActions.checkAuthFailed(e))
    }
}