import AuthPage from "./components/AuthPage";
import LoginForm from "./components/LoginForm";
import RegistrationForm from "./components/RegistrationForm";
import {userActions, userReducer } from "./store/reducer";
import {loginAction} from "./store/actions";

export {
    AuthPage, RegistrationForm, LoginForm,
    userReducer, userActions,
    loginAction
};