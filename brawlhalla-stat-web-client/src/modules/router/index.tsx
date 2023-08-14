import {createBrowserRouter} from "react-router-dom";
import SandBoxPage from "../../App/Components/SandBoxPage";
import AuthPage from "../authentication/AuthPage";

export const router = createBrowserRouter([
    {
        path: "/auth",
        element: <AuthPage/>
    },
    {
        path: "/",
        element: <SandBoxPage/>,
    }
]);