import React from 'react';
import {getTheme} from "./modules/theme";
import {ThemeProvider} from "@mui/material/styles";
import {useSelector} from "react-redux";
import {RootState} from "./store/rootReducer";
import {Grid} from "@mui/material";
import Layout from "./App/Components/Layout";
import { AuthTabs } from './modules/authentication';

interface AppProps {
}

const App : React.FC<AppProps> = () => {
    const themeMode = useSelector((state: RootState) => state.theme.mode);
    const theme = getTheme(themeMode);

    return (
        <ThemeProvider theme={theme}>
            <Layout>
                <Grid item xs={12} md={6}>
                    <AuthTabs/>
                </Grid>
                {/*<Grid item xs={12} md={6}>*/}
                {/*    /!* Your content here *!/*/}
                {/*    /!* This grid item will take 6 columns on medium screens and full width on small screens *!/*/}
                {/*</Grid>*/}
            </Layout>
        </ThemeProvider>
    );
}

export default App;