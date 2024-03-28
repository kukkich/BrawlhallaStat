import {FC} from 'react';
import {StatisticFilterBase} from "../../types";
import {useBrawlhallaEntities} from "../../../brawlhallaEntities/hooks/useBrawlhallaEntities";
import {Container, Grid, Paper, styled, Typography} from "@mui/material";
import {GameType} from "../../../brawlhallaEntities/types";

type Props = {
    filter: StatisticFilterBase
};

type GameViewProps = {
    gameType: GameType | null
}
const CenteredBox = styled('div')({
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
});

const VerticalTypography = styled(Typography)({
    writingMode: 'vertical-rl',
    transform: 'rotate(-180deg)',
});

const GameView: FC<GameViewProps> = ({gameType}) => {
    const getGameMode = (gameType: GameType | null): string => {
        if (gameType === null) {
            return 'Any';
        } else if (gameType === GameType.unranked1V1 || gameType === GameType.unranked2V2) {
            return 'Unranked'
        } else if (gameType === GameType.ranked1V1 || gameType === GameType.ranked2V2) {
            return 'Ranked'
        }
        throw new Error('Unexpected game type. Cant determine game mode.')
    }
    const getTeamSizeInfo = (gameType: GameType | null): string | null => {
        if (gameType === null) {
            return null;
        } else if (gameType === GameType.unranked1V1 || gameType === GameType.ranked1V1) {
            return '1 vs 1'
        } else if (gameType === GameType.unranked2V2 || gameType === GameType.ranked2V2) {
            return '2 vs 2'
        }
        throw new Error('Unexpected game type. Cant determine team size.')
    }
    // return (
    //     <CenteredBox>
    //         <VerticalTypography>
    //             Unranked
    //         </VerticalTypography>
    //         <VerticalTypography>
    //             1v1
    //         </VerticalTypography>
    //     </CenteredBox>
    // );

    const gameMode = getGameMode(gameType)
    const teamSizeInfo = getTeamSizeInfo(gameType);

    // let textContent;
    // if (gameType === null) {
    //     textContent = (<Typography>Any</Typography>)
    // } else if (gameType === GameType.unranked1V1) {
    //     textContent = (<Typography>Unranked 1V1</Typography>)
    // } else if (gameType === GameType.ranked1V1) {
    //     textContent = (<Typography>Ranked 1V1</Typography>)
    // } else if (gameType === GameType.unranked2V2) {
    //     textContent = (<Typography>Unranked 2V2</Typography>)
    // } else if (gameType === GameType.ranked2V2) {
    //     textContent = (<Typography>Ranked 2V2</Typography>)
    // }

    return (
        <CenteredBox>
            <VerticalTypography>
                {gameMode}
            </VerticalTypography>
            {teamSizeInfo !== null
                ?
                <VerticalTypography>
                    {teamSizeInfo}
                </VerticalTypography>
                : null
            }
        </CenteredBox>
    )
}

export const FilterView: FC<Props> = ({filter}: Props) => {
    const [weapons, legends, fetched] = useBrawlhallaEntities(true);

    const legend = filter.legendId !== null
        ? legends.find(x => x.id === filter.legendId)
        : null

    return (
        <Paper elevation={8}>
            <Grid container
                  // spacing={2}
            >
                <Grid item>
                    <Container sx={{width: 28}}>
                        <GameView gameType={filter.gameType}></GameView>
                        {/*{fetched*/}
                        {/*    ? (legend !== null && legend !== undefined*/}
                        {/*        ? <LegendIcon width='40' name={legend.name} />*/}
                        {/*        : <CircularProgress size={20} color="warning"/>)*/}
                        {/*    : <CircularProgress size={20} color="inherit"/>*/}
                        {/*}*/}
                    </Container>
                </Grid>
                <Grid item xs={12} sm container>
                    <Grid item xs container direction="column" spacing={2}>
                        <Grid item xs>
                            <Typography gutterBottom variant="subtitle1" component="div">
                                Standard license
                            </Typography>
                            <Typography variant="body2" gutterBottom>
                                Full resolution 1920x1080 • JPEG
                            </Typography>
                            <Typography variant="body2" color="text.secondary">
                                ID: 1030114
                            </Typography>
                        </Grid>
                        <Grid item>
                            <Typography sx={{cursor: 'pointer'}} variant="body2">
                                Remove
                            </Typography>
                        </Grid>
                    </Grid>
                    <Grid item>
                        <Typography variant="subtitle1" component="div">
                            $19.00
                        </Typography>
                    </Grid>
                </Grid>
            </Grid>

        </Paper>
    );
};