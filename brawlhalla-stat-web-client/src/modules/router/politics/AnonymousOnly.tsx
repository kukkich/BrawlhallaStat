import React from "react";
import {Navigate, useLocation} from "react-router-dom";
import {useRootSelector} from "../../../store";
import {LoginStatus} from "../../authentication/store/state";
import {IPolicyComponent} from "./IPolicyComponent";

export const AnonymousOnly: IPolicyComponent = ({children}) => {
    const userState = useRootSelector(state => state.userReducer);
    const isAuth = userState.status === LoginStatus.authorized
    let location = useLocation();

    if (isAuth) {
        return <Navigate to="/" state={{from: location}} replace/>;
    }

    return <>{children}</>;
}