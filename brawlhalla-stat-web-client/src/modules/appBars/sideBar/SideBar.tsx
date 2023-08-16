import React from "react";
import {Grid, List, ListItemButton, ListItemIcon, ListItemText, Paper} from "@mui/material";

const SideBar: React.FC = () => {
    return (
        <Grid item xs={2} sx={{height: '100vh'}}>
            <Paper elevation={2} sx={{height: '100vh'}}>
                <List>
                    <ListItemButton>
                        <ListItemIcon/>
                        <ListItemText primary="Sidebar Item"/>
                    </ListItemButton>
                </List>
            </Paper>
        </Grid>
    );
};

export default SideBar;
