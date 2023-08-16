import React from 'react';
import {IPolicyComponent} from "./IPolicyComponent";

const EmptyPolicy: IPolicyComponent = ({children}) => {
    return <>{children}</>;
};

export default EmptyPolicy;