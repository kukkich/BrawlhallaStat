import React from 'react';
import {getTheme} from "./modules/theme";
import {ThemeProvider} from "@mui/material/styles";
import Layout from "./App/Components/Layout";
import {useRootSelector} from "./store";
import {router} from "./modules/router";
import {Route, RouterProvider, Routes} from "react-router-dom";
import {AuthPage} from "./modules/authentication";
import SandBoxPage from "./App/Components/SandBoxPage";
import {AuthRequired} from "./modules/router/politics/AuthRequired";

interface AppProps {
}

const App: React.FC<AppProps> = () => {
    const themeMode = useRootSelector(state => state.themeReducer.mode);
    const theme = getTheme(themeMode);

    return (
        <ThemeProvider theme={theme}>
            <Routes>
                <Route element={<Layout />}>
                    <Route path="/" element={<SandBoxPage />} />
                    <Route path="/auth" element={<AuthPage />} />
                    <Route
                        path="/protected"
                        element={
                            <AuthRequired>
                                <div>Protected</div>
                            </AuthRequired>
                        }
                    />
                </Route>
            </Routes>
            {/*<Layout>*/}
            {/*    <RouterProvider router={router} />*/}
            {/*</Layout>*/}
        </ThemeProvider>
    );
}

export default App;