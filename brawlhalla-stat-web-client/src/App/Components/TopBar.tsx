import React from 'react';
import {AppBar, Grid, IconButton, Switch, Toolbar, Typography} from "@mui/material";
import {Menu as MenuIcon} from "@mui/icons-material";
import Button from "@mui/material/Button";
import {useDispatch, useSelector} from "react-redux";
import {toggleTheme} from "../../modules/theme";
import {ThemeMode} from "../../modules/theme/types";
import {RootState} from "../../store/rootReducer";
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
            // <AppBar position="static">
            //     <Toolbar>
            //         <IconButton color="inherit">
            //             <MenuIcon />
            //         </IconButton>
            //         {/* Add your header content here */}
            //     </Toolbar>
            // </AppBar>
    );
};

export default TopBar;