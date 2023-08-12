import {createSlice} from '@reduxjs/toolkit';
import {ThemeMode, ThemeState} from "./types";

const getDefaultTheme = () : ThemeMode => {
    const prefersDarkMode : boolean = window.matchMedia('(prefers-color-scheme: darkTheme)').matches
    if(prefersDarkMode)
        return ThemeMode.Dark
    else return ThemeMode.Light
}

const initialState: ThemeState = {
    mode: getDefaultTheme()
};

const themeSlice = createSlice({
    name: 'theme',
    initialState,
    reducers: {
        toggleTheme(state) {
            if (state.mode === ThemeMode.Light) {
                state.mode = ThemeMode.Dark;
            } else {
                state.mode = ThemeMode.Light;
            }
        },
    },
});

export const themeReducer = themeSlice.reducer;
export const themeActions = themeSlice.actions;
