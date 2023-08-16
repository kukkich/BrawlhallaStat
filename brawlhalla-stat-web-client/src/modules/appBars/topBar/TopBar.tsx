import React from 'react';
import {AppBar, Grid, IconButton, Toolbar} from "@mui/material";
import {Menu as MenuIcon} from "@mui/icons-material";
import Button from "@mui/material/Button";
import {ThemeSwitch} from "../../../App/Components/ThemeSwitch";
import {Link} from 'react-router-dom';
import AvatarMenu from "./components/AvatarMenu";
import {useRootSelector} from "../../../store";
import {LoginStatus} from "../../authentication/store/State";
import LoginButton from "./components/LoginButton";
import Logo from "./components/Logo";

const TopBar: React.FC = () => {
    const userState = useRootSelector(state => state.userReducer);
    const isAuth = userState.status === LoginStatus.authorized;

    return (
        <Grid item xs={12}>
            <AppBar position="static">
                <Toolbar>
                    <IconButton
                        size="large"
                        edge="start"
                        color="inherit"
                        aria-label="menu"
                        sx={{ mr: 2 }}
                    >
                        <MenuIcon />
                    </IconButton>
                    <Logo/>

                    <Button component={Link} to="/" color="inherit">sandbox</Button>
                    <Button component={Link} to="/protected" color="inherit">Protected</Button>

                    <ThemeSwitch/>

                    {isAuth
                        ? <AvatarMenu/>
                        : <LoginButton/>
                    }
                </Toolbar>
            </AppBar>
        </Grid>
    );
};

export default TopBar;