import React, {useEffect} from 'react';
import {getTheme} from "./modules/theme";
import {ThemeProvider} from "@mui/material/styles";
import {useRootDispatch, useRootSelector} from "./store";
import RouterView from "./modules/router/RouterView";
import {checkAuthAction} from "./modules/authentication/store/actions";

interface AppProps {
}

const App: React.FC<AppProps> = () => {
    const themeMode = useRootSelector(state => state.themeReducer.mode);
    const theme = getTheme(themeMode);

    const dispatch = useRootDispatch();

    useEffect(() =>{
        if (localStorage.getItem('token')){
            dispatch(checkAuthAction())
        }
    }, [])

    return (
        <ThemeProvider theme={theme}>
            <RouterView/>
        </ThemeProvider>
    );
}

export default App;