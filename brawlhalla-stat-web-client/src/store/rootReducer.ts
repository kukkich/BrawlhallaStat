import { combineReducers } from 'redux';
import {themeReducer} from "../modules/theme";

const rootReducer = combineReducers({
    theme: themeReducer,
    // user: userReducer,
});

export type RootState = ReturnType<typeof rootReducer>
export default rootReducer;