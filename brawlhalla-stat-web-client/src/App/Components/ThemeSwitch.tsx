import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import Box from '@mui/material/Box';
import Brightness4Icon from '@mui/icons-material/Brightness4';
import Brightness7Icon from '@mui/icons-material/Brightness7';
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "../../store/rootReducer";
import {toggleTheme} from "../../modules/theme";
import {ThemeMode} from "../../modules/theme/types";

export const ThemeSwitch: React.FC = () => {
    const dispatch = useDispatch();
    const themeMode = useSelector((state: RootState) => state.theme.mode);
    const handleThemeToggle = () => dispatch(toggleTheme())

    return (
        <Box
            sx={{
                justifyContent: 'center',
                pr: 4
            }}
        >
            <IconButton onClick={handleThemeToggle} color="inherit">
                {themeMode === ThemeMode.Dark ? <Brightness7Icon /> : <Brightness4Icon />}
            </IconButton>
        </Box>
    );
}