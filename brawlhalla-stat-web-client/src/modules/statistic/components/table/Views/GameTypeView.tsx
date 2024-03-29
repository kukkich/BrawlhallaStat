import {GameType} from "../../../../brawlhallaEntities/types";
import {styled, Typography} from "@mui/material";
import {FC} from "react";

type GameViewProps = {
    gameType: GameType | null
}

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

const CenteredBox = styled('div')({
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
});

const VerticalTypography = styled(Typography)({
    writingMode: 'vertical-rl',
    transform: 'rotate(-180deg)',
    alignSelf: 'center'
});

export const GameTypeView: FC<GameViewProps> = ({gameType}) => {
    const gameMode = getGameMode(gameType)
    const teamSizeInfo = getTeamSizeInfo(gameType);

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