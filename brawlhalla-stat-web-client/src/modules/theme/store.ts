import {createSlice} from '@reduxjs/toolkit';
import {ThemeMode, ThemeState} from "./types";

const getDefaultTheme = () : ThemeMode => {
    const savedTheme = localStorage.getItem('theme');
    console.log(savedTheme);
    if (savedTheme)
       return parseInt(savedTheme) as ThemeMode;

    const prefersDarkMode : boolean = window.matchMedia('(prefers-color-scheme: darkTheme)').matches
    if(prefersDarkMode)
        return ThemeMode.Dark
    else return ThemeMode.Light
}
const saveTheme = (themeMode: ThemeMode) => {
    localStorage.setItem('theme', themeMode.toString());
}
const initThemeState = () : ThemeMode => {
    const theme = getDefaultTheme()
    saveTheme(theme)
    return theme
}



const initialState: ThemeState = {
    mode: initThemeState()
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
