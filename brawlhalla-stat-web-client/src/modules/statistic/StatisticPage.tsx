import {Grid, useTheme} from '@mui/material';
import React from 'react';
import SideBar from "../appBars/sideBar/SideBar";
import {LegendsStatisticContent} from "./legend";

const StatisticPage = () => {
    const theme = useTheme();
    return (
        <Grid container spacing={2} sx={{height: '100%'}}>
            <SideBar/>
            <Grid item xs={10} sx={{mt: theme.spacing(2)}}>
                <LegendsStatisticContent/>
            </Grid>
            <Grid item/>
        </Grid>

    );
};

export default StatisticPage;