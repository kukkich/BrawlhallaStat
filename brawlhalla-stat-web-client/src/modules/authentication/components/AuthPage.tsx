import React from 'react';
import {Grid, Paper, Theme, useMediaQuery} from '@mui/material';
import AuthTabs from "./AuthTabs";
import { styled } from '@mui/material/styles';

const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: 'center',
    color: theme.palette.text.secondary,
}));

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
