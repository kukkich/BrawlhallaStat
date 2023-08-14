import React from 'react';
import {getTheme} from "./modules/theme";
import {ThemeProvider} from "@mui/material/styles";
import Layout from "./App/Components/Layout";
import {useRootSelector} from "./store";
import {router} from "./modules/router";
import {RouterProvider} from "react-router-dom";

interface AppProps {
}

const App: React.FC<AppProps> = () => {
    const themeMode = useRootSelector(state => state.themeReducer.mode);
    const theme = getTheme(themeMode);

    return (
        <ThemeProvider theme={theme}>
            <Layout>
                <RouterProvider router={router} />
            </Layout>
        </ThemeProvider>
    );
}

export default App;