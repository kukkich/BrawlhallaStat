import {createTheme} from "@mui/material/styles";
import {Theme} from "@mui/material";

const darkTheme: Theme = createTheme({
    palette: {
        mode: 'dark',
        primary: {
            main: '#2317af',
            //#1706d6
        },
        secondary: {
            main: '#f50057',
        },
    },
});

export default darkTheme;
