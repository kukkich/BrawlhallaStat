import React from 'react';
import {Grid} from '@mui/material';
import TopBar from "../../modules/appBars/topBar/TopBar";
import SideBar from "./SideBar";
import { Outlet } from 'react-router-dom';

const Layout: React.FC = () => {
    return (
        <Grid container sx={{
            backgroundColor: 'background.paper',
            color: 'white',
        }}>
            <TopBar/>

            <SideBar/>

            {/* Main Content */}
            <Grid item xs={10} >
                {/*TODO Error source*/}
                <Grid container xs={10}>
                    <Outlet />
                </Grid>
            </Grid>
        </Grid>
    );
};

export default Layout;