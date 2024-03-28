import AuthService from "../services/AuthService";
import {AppDispatch} from "../../../store";
import {userActions} from "./reducer";
import {LoginRequest, RegisterRequest} from "../types";
import {getErrorResponse} from "../../../api/tools/getErrorResponse";

export const loginAction = (request: LoginRequest) => async (dispatch: AppDispatch) => {
    try {
        dispatch(userActions.loginStart())
        const response = await AuthService.login(request)
        dispatch(userActions.loginSuccess(response.data));
    } catch (e: any) {
        dispatch(userActions.loginFailed(getErrorResponse(e).errors))
    }
}

export const registerAction = (request: RegisterRequest) => async (dispatch: AppDispatch) => {
    try {
        dispatch(userActions.loginStart())
        const response = await AuthService.register(request)
        dispatch(userActions.loginSuccess(response.data));
    } catch (e: any) {
        dispatch(userActions.loginFailed(getErrorResponse(e).errors))
    }
}

export const logoutAction = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(userActions.logoutStart())
        await AuthService.logout()
        dispatch(userActions.logoutFinished());
    } catch (e: any) {
        dispatch(userActions.logoutFinished())
    }
}

export const checkAuthAction = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(userActions.checkAuthStart())
        const response = await AuthService.refresh();
        dispatch(userActions.loginSuccess(response.data));
    } catch (e) {
        dispatch(userActions.checkAuthFailed())
    }
}