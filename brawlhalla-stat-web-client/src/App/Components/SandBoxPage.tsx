import React from "react";
import {Container, Grid} from "@mui/material";
import {StatisticTable} from "../../modules/statistic/components/table/StatisticTable";

const SandBoxPage: React.FC = () => {
    return (
        <Grid item container justifyContent="center" alignItems="center">
            <Container>
                <StatisticTable/>
            </Container>
        </Grid>

    );
};

export default SandBoxPage