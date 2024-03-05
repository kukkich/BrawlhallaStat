import React from "react";
import {Grid} from "@mui/material";
import {GetEntitiesButton} from "../../modules/brawlhallaEntities/components/getEntitiesButton";
import {LegendSelect} from "../../modules/brawlhallaEntities/components/LegendSelect";

const SandBoxPage: React.FC = () => {
    return (
        <Grid item container justifyContent="center" alignItems="center">
            <LegendSelect legendChange={_ => {}}/>
            <GetEntitiesButton/>
        </Grid>

    );
};

export default SandBoxPage