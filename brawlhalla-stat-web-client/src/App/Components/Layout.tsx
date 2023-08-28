import React from 'react';
import {Grid} from '@mui/material';
import TopBar from "../../modules/appBars/topBar/TopBar";
import {Outlet} from 'react-router-dom';

const Layout: React.FC = () => {
    return (
        <Grid container
              sx={{
                  backgroundColor: 'background.paper',
                  color: 'white',
              }}
              columnSpacing={{xs: 1, sm: 2, md: 3}}
        >
            <TopBar/>

            {/* Main Content */}
            <Grid item xs={12} sx={{height: "100vh"}}>
                <Outlet/>
            </Grid>
        </Grid>
    );
};

export default Layout;