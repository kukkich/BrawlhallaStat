import React from "react";
import {Container, Grid} from "@mui/material";
import {GetEntitiesButton} from "../../modules/brawlhallaEntities/components/getEntitiesButton";
import {LegendSelect} from "../../modules/brawlhallaEntities/components/LegendSelect";
import {WeaponSelect} from "../../modules/brawlhallaEntities/components/WeaponSelect";

const SandBoxPage: React.FC = () => {
    return (
        <Grid item container justifyContent="center" alignItems="center">
            <Container>
                <LegendSelect legendChange={_ => {}}/>
                <WeaponSelect weaponChange={_ => {}}/>
            </Container>
            <GetEntitiesButton/>
        </Grid>

    );
};

export default SandBoxPage