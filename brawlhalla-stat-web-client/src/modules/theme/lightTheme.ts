import { createTheme } from '@mui/material/styles';
import {Theme} from "@mui/material";

const lightTheme : Theme = createTheme({
    palette: {
        mode: 'light',
        primary: {
            main: '#2317af',
        },
        secondary: {
            main: '#f50057',
        },
    },
    typography: {
        fontFamily: 'monospace',
    },
});

export default lightTheme;
