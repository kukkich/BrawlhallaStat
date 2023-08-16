import React from 'react';
import {getTheme} from "./modules/theme";
import {ThemeProvider} from "@mui/material/styles";
import {useRootSelector} from "./store";
import RouterView from "./modules/router/RouterView";

interface AppProps {
}

const App: React.FC<AppProps> = () => {
    const themeMode = useRootSelector(state => state.themeReducer.mode);
    const theme = getTheme(themeMode);

    return (
        <ThemeProvider theme={theme}>
            <RouterView/>
        </ThemeProvider>
    );
}

export default App;