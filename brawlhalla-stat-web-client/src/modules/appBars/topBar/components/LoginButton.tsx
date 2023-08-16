import React from 'react';
import {Link} from "react-router-dom";
import Button from "@mui/material/Button";

const LoginButton: React.FC = () => {
    return (
        <Button component={Link} to="/auth" color="inherit">Login</Button>
    );
};

export default LoginButton;