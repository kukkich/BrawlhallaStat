import {Grid, useTheme} from '@mui/material';
import React, {FC} from 'react';
import SideBar from "../appBars/sideBar/SideBar";
import {StatisticSideBarContent} from "./components/StatisticSideBarContent";
import {StatisticTable} from "./components/table/StatisticTable";

const StatisticPage: FC = () => {
    const theme = useTheme();
    return (
        <Grid container spacing={2} sx={{height: '100%'}}>
            <SideBar content={<StatisticSideBarContent/>}/>
            <Grid item xs={10} sx={{mt: theme.spacing(2)}}>
                <StatisticTable/>
                {/*<FilterForm onSubmit={() => {}}/>*/}
            </Grid>
            <Grid item/>
        </Grid>

    );
};

export default StatisticPage;