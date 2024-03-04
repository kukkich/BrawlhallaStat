import React from "react";
import {Grid, Typography} from "@mui/material";
import {GetEntitiesButton} from "../../modules/brawlhallaEntities/components/getEntitiesButton";

const SandBoxPage: React.FC = () => {
    return (
        <Grid item container justifyContent="center" alignItems="center"
        >
            <GetEntitiesButton/>
        </Grid>
    );
};

export default SandBoxPage