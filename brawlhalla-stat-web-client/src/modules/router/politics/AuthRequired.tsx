import React from "react";
import {Navigate, useLocation} from "react-router-dom";
import {useRootSelector} from "../../../store";
import {LoginStatus} from "../../authentication/store/state";
import {IPolicyComponent} from "./IPolicyComponent";

export const AuthRequired: IPolicyComponent = ({children}) => {
    const userState = useRootSelector(state => state.userReducer);
    const isAuth = userState.status !== LoginStatus.unauthorized
    let location = useLocation();

    if (!isAuth) {
        return <Navigate to="/auth" state={{from: location}} replace/>;
    }

    return <>{children}</>;
}