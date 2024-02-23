import {Theme} from "@mui/material";
import {ThemeMode} from "./types";
import {darkTheme, lightTheme} from "./index";

const themeDictionary = new Map<ThemeMode, Theme>();
themeDictionary.set(ThemeMode.Light, lightTheme);
themeDictionary.set(ThemeMode.Dark, darkTheme);

const getTheme = (mode: ThemeMode) : Theme => {
    const theme = themeDictionary.get(mode);
    if (theme === undefined)
        throw new Error("Темы не существует");
    return theme;
}

export default getTheme;