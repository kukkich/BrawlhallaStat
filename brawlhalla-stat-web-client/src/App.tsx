import React from 'react';
import {getTheme} from "./modules/theme";
import {ThemeProvider} from "@mui/material/styles";
import Layout from "./App/Components/Layout";
import {AuthPage} from './modules/authentication';
import {useRootSelector} from "./store";

interface AppProps {
}

const App: React.FC<AppProps> = () => {
    const themeMode = useRootSelector(state => state.themeReducer.mode);
    const theme = getTheme(themeMode);

    return (
        <ThemeProvider theme={theme}>
            <Layout>
                <AuthPage/>
            </Layout>
        </ThemeProvider>
    );
}

export default App;