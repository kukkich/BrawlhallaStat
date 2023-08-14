import React from "react";
import {Grid, Theme, useMediaQuery} from "@mui/material";

const SandBoxPage: React.FC = () => {
    const isLargerMd = useMediaQuery((theme : Theme) => theme.breakpoints.up('md'));
    const topMargin = isLargerMd ? '10%' : '8px';

    return (
        <Grid item container
              justifyContent="center" alignItems="center"
              sx={{mt: topMargin}}
        >
            квак
        </Grid>
    );
};

export default SandBoxPage