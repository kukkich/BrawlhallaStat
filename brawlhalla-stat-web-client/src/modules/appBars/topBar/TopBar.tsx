import React from 'react';
import {AppBar, CircularProgress, Grid, IconButton, Toolbar, Typography} from "@mui/material";
import {Menu as MenuIcon} from "@mui/icons-material";
import Button from "@mui/material/Button";
import {ThemeSwitch} from "../../../App/Components/ThemeSwitch";
import {useNavigate} from 'react-router-dom';
import AvatarMenu from "./components/AvatarMenu";
import {useRootSelector} from "../../../store";
import {LoginStatus} from "../../authentication/store/State";
import LoginButton from "./components/LoginButton";
import Logo from "./components/Logo";
import Box from "@mui/material/Box";

interface Page {
    name: string,
    path: string
}

const pages: Page[] = [
    {name: "Sandbox", path: "/"},
    {name: "Stats", path: "/statistic"},
    {name: "Protected", path: "/protected"},
]

const TopBar: React.FC = () => {
    const userState = useRootSelector(state => state.userReducer);
    const navigate = useNavigate();

    return (
        <Grid item xs={12}>
            <AppBar position={"static"}>
                <Toolbar>
                    <IconButton
                        size="large"
                        edge="start"
                        color="inherit"
                        aria-label="menu"
                        sx={{mr: 2}}
                    >
                        <MenuIcon/>
                    </IconButton>
                    <Logo/>
                    {pages.map((page) => (
                        <Button
                            key={page.name}
                            color="inherit"
                            onClick={() => navigate(page.path)}
                            sx={{ml: 2}}
                        >
                            <Typography variant="h6" component="div" sx={{
                                display: { xs: 'none', md: 'flex' },
                                fontFamily: 'monospace',
                                // fontWeight: 700,
                                // letterSpacing: '.3rem',
                                color: 'inherit',
                                textDecoration: 'none',
                                textTransform: 'none'
                            }}>
                                {page.name}
                            </Typography>
                        </Button>
                    ))}

                    <Box sx={{flexGrow: 1}}/>
                    <ThemeSwitch/>

                    {userState.status === LoginStatus.authorized
                        ? <AvatarMenu/>
                        : userState.status === LoginStatus.authChecking
                            ? <CircularProgress color="inherit"/>
                            : <LoginButton/>
                    }
                </Toolbar>
            </AppBar>
        </Grid>
    );
};

export default TopBar;