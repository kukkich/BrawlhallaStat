import React from 'react';
import {Grid, Theme, useMediaQuery} from '@mui/material';
import AuthTabs from "./components/AuthTabs";

const AuthPage: React.FC = () => {
    const isLargerMd = useMediaQuery((theme : Theme) => theme.breakpoints.up('md'));
    const topMargin = isLargerMd ? '10%' : '8px';

    return (
        <Grid item container
              justifyContent="center" alignItems="center"
              sx={{mt: topMargin}}
        >
            <Grid item xs={10} md={6}
                  container direction="column"
                  justifyContent="space-evenly" alignItems="flex-end"
            >
                <AuthTabs />
            </Grid>
        </Grid>
    );
};

export default AuthPage;
