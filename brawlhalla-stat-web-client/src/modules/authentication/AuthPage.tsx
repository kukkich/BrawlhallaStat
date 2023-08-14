import React from 'react';
import {Grid, Theme, useMediaQuery} from '@mui/material';
import AuthTabs from "./components/AuthTabs";
import {useRootSelector} from "../../store";
import {LoginStatus} from "./store/State";
import {Navigate} from "react-router-dom";

const AuthPage: React.FC = () => {
    const isLargerMd = useMediaQuery((theme: Theme) => theme.breakpoints.up('md'));
    const topMargin = isLargerMd ? '10%' : '8px';
    const userState = useRootSelector(state => state.userReducer);

    if (userState.status === LoginStatus.authorized) {
        return <Navigate to="/" replace></Navigate>
    }

    return (
        <Grid item container
              justifyContent="center" alignItems="center"
              sx={{mt: topMargin}}
        >
            <Grid item xs={10} md={6}
                  container direction="column"
                  justifyContent="space-evenly" alignItems="flex-end"
            >
                <AuthTabs/>
            </Grid>
        </Grid>
    );
};

export default AuthPage;
