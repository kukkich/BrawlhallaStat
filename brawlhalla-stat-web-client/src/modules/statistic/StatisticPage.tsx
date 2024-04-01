import {Grid, useTheme} from '@mui/material';
import React, {FC} from 'react';
import {StatisticTable} from "./components/table/StatisticTable";

const StatisticPage: FC = () => {
    const theme = useTheme();
    return (
        <Grid container spacing={2} sx={{height: '100%'}}>
            {/*<SideBar content={<StatisticSideBarContent/>}/>*/}
            <Grid item xs/>
            <Grid item xs={8} sx={{mt: theme.spacing(2)}}>
                <StatisticTable/>
            </Grid>
            <Grid item xs/>
        </Grid>

    );
};

export default StatisticPage;