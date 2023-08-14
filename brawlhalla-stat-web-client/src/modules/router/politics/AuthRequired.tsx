import React from "react";
import {Navigate, useLocation} from "react-router-dom";
import {useRootSelector} from "../../../store";
import {LoginStatus} from "../../authentication/store/State";

interface PrivateRouteProps {
    children: React.ReactNode;
}

export const RequireAuth: React.FC<PrivateRouteProps> = ({children}) => {
    const userState = useRootSelector(state => state.userReducer);
    const isAuth = userState.status !== LoginStatus.unauthorized
    let location = useLocation();

    if (!isAuth) {
        return <Navigate to="/login" state={{from: location}} replace/>;
    }

    return <>{children}</>;
}