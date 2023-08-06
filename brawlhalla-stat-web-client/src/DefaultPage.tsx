import React from 'react';
import Layout from './App/Components/Layout';
import {Grid} from "@mui/material";

const MyPage: React.FC = () => {
    return (
        <Layout>
            <Grid item xs={12} md={6}>
                {/* Your content here */}
                {/* This grid item will take 6 columns on medium screens and full width on small screens */}
            </Grid>
            <Grid item xs={12} md={6}>
                {/* Your content here */}
                {/* This grid item will take 6 columns on medium screens and full width on small screens */}
            </Grid>
        </Layout>
    );
};

export default MyPage;

