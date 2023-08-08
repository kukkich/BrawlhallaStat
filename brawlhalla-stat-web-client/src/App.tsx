import React from 'react';
import {getTheme} from "./modules/theme";
import {ThemeProvider} from "@mui/material/styles";
import {useSelector} from "react-redux";
import {RootState} from "./store/rootReducer";
import Layout from "./App/Components/Layout";
import {AuthPage} from './modules/authentication';

interface AppProps {
}

const App: React.FC<AppProps> = () => {
    const themeMode = useSelector((state: RootState) => state.theme.mode);
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