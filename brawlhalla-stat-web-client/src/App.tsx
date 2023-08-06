import React from 'react';
import TopBar from "./App/Components/TopBar";
import {getTheme} from "./modules/theme";
import {ThemeProvider} from "@mui/material/styles";
import {useSelector} from "react-redux";
import {RootState} from "./store/rootReducer";

interface AppProps {
}

const App : React.FC<AppProps> = () => {
    const themeMode = useSelector((state: RootState) => state.theme.mode);
    const theme = getTheme(themeMode);

    return (
        <ThemeProvider theme={theme}>
            {/*<DefaultPage/>*/}
            <TopBar/>
        </ThemeProvider>
    );
}

export default App;