import React from 'react';
import {AppBar, Grid, IconButton, Toolbar, Typography} from "@mui/material";
import {Menu as MenuIcon} from "@mui/icons-material";
import Button from "@mui/material/Button";
import {ThemeSwitch} from "./ThemeSwitch";

const TopBar: React.FC = () => {
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
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        BHStats
                    </Typography>
                    <ThemeSwitch/>
                    <Button color="inherit">Login</Button>
                </Toolbar>
            </AppBar>
        </Grid>
    );
};

export default TopBar;