import {FC} from 'react';
import {StatisticFilterBase} from "../../../types";
import {useBrawlhallaEntities} from "../../../../brawlhallaEntities/hooks/useBrawlhallaEntities";
import {Container, Grid, Paper, styled, Typography} from "@mui/material";
import {GameTypeView} from "./GameTypeView";
import {EntitiesSetupView} from "./EntitiesSetupView";

type Props = {
    filter: StatisticFilterBase
};

const CenterGrid = styled(Grid)({
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center'
})

export const FilterView: FC<Props> = ({filter}: Props) => {
    const [weapons, legends, fetched] = useBrawlhallaEntities(true);

    const legend = filter.legendId !== null
        ? legends.find(x => x.id === filter.legendId)
        : null

    return (
        // <Paper elevation={8} sx={{py: 1}}>
            <Grid container
                // spacing={2}
            >
                <CenterGrid item>
                    <Container sx={{width: 28}}>
                        <GameTypeView gameType={filter.gameType}></GameTypeView>
                        {/*{fetched*/}
                        {/*    ? (legend !== null && legend !== undefined*/}
                        {/*        ? <LegendIcon width='40' name={legend.name} />*/}
                        {/*        : <CircularProgress size={20} color="warning"/>)*/}
                        {/*    : <CircularProgress size={20} color="inherit"/>*/}
                        {/*}*/}
                    </Container>
                </CenterGrid>
                <CenterGrid item>
                    <EntitiesSetupView size='60' filter={filter}/>
                </CenterGrid>
            </Grid>

        // </Paper>
    );
};