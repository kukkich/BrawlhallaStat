import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import Box from '@mui/material/Box';
import Brightness4Icon from '@mui/icons-material/Brightness4';
import Brightness7Icon from '@mui/icons-material/Brightness7';
import {useDispatch} from "react-redux";
import {ThemeMode} from "../../modules/theme/types";
import {themeActions} from "../../modules/theme";
import {useRootSelector} from "../../store";

export const ThemeSwitch: React.FC = () => {
    const dispatch = useDispatch();
    const {toggleTheme} = themeActions;

    const themeMode = useRootSelector((state) => state.themeReducer.mode);
    const handleThemeToggle = () => dispatch(toggleTheme())

    return (
        <Box
        >
            <IconButton onClick={handleThemeToggle} color="inherit">
                {themeMode === ThemeMode.Dark ? <Brightness7Icon /> : <Brightness4Icon />}
            </IconButton>
        </Box>
    );
}