import React from "react";
import {Navigate, useLocation} from "react-router-dom";
import {useRootSelector} from "../../../store";
import {LoginStatus} from "../../authentication/store/State";

interface PrivateRouteProps {
    children: React.ReactNode;
}
// TODO Убрать редирект в странице логина, обернуть логин в эту политику
export const AnonymousOnly: React.FC<PrivateRouteProps> = ({children}) => {
    const userState = useRootSelector(state => state.userReducer);
    const isAuth = userState.status === LoginStatus.authorized
    let location = useLocation();

    if (isAuth) {
        return <Navigate to="/" state={{from: location}} replace/>;
    }

    return <>{children}</>;
}