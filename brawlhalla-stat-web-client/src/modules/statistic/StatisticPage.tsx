import {Grid, useTheme} from '@mui/material';
import React from 'react';
import SideBar from "../appBars/sideBar/SideBar";
import {LegendsStatisticContent} from "./legend";

const StatisticPage = () => {
    const theme = useTheme();
    return (
        <Grid container spacing={2}>
            <SideBar/>
            <Grid item xs sx={{mt: theme.spacing(2)}}>
                <LegendsStatisticContent/>
            </Grid>
            <Grid item/>
        </Grid>

    );
};

export default StatisticPage;