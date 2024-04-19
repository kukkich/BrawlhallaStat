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
        text: {
            primary: '#000', // цвет текста для светлой темы
            secondary: '#555' // подойдет для менее выделяющегося текста
        }
    },
    typography: {
        fontFamily: 'monospace',
    },
});

export default lightTheme;
