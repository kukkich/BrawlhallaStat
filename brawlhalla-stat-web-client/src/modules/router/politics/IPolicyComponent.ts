import {FC, ReactNode} from "react";

export interface PrivateRouteProps {
    children: ReactNode;
}

export interface IPolicyComponent extends FC<PrivateRouteProps> {

}