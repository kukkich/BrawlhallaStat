import React from 'react';
import {Grid, Paper} from '@mui/material';
import TopBar from "./TopBar";
import SideBar from "./SideBar";

interface LayoutProps {
    children: React.ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
    return (
        <Grid container sx={{
            backgroundColor: 'background.paper',
            color: 'white',
        }}>
            <TopBar/>

            <SideBar/>

            {/* Main Content */}
            <Grid item xs={10} >
                <Grid container xs={10}>
                    {/* Content with 12 columns */}
                    {children}
                </Grid>
            </Grid>
        </Grid>
    );
};

export default Layout;