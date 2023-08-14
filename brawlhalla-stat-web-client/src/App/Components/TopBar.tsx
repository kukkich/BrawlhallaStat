import React from 'react';
import {AppBar, Grid, IconButton, Toolbar, Typography} from "@mui/material";
import {Menu as MenuIcon} from "@mui/icons-material";
import Button from "@mui/material/Button";
import {ThemeSwitch} from "./ThemeSwitch";
import { Link } from 'react-router-dom';

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
                    <Button component={Link} to="/" color="inherit">sandbox</Button>
                    <Button component={Link} to="/protected" color="inherit">Protected</Button>

                    <ThemeSwitch/>
                    <Button component={Link} to="/auth" color="inherit">Login</Button>
                </Toolbar>
            </AppBar>
        </Grid>
    );
};

export default TopBar;