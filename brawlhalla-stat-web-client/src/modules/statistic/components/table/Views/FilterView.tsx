import {FC} from 'react';
import {StatisticFilterBase} from "../../../types";
import {Container, Grid, styled} from "@mui/material";
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
    return (
            <Grid container
                spacing={1}
            >
                <CenterGrid item>
                    <Container sx={{width: 28}}>
                        <GameTypeView gameType={filter.gameType}></GameTypeView>
                    </Container>
                </CenterGrid>
                <CenterGrid item>
                    <EntitiesSetupView size='40' filter={filter}/>
                </CenterGrid>
            </Grid>
    );
};