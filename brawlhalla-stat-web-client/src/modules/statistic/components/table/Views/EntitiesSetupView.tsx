import {FC} from 'react';
import {StatisticFilterBase} from "../../../types";
import {CircularProgress, Divider, Paper, Stack, styled} from "@mui/material";
import {Legend, Weapon} from "../../../../brawlhallaEntities/types";
import {LegendIcon} from "../../../../brawlhallaEntities/components/icons/LegendIcon";
import {useBrawlhallaEntities} from "../../../../brawlhallaEntities/hooks/useBrawlhallaEntities";
import {WeaponIcon} from "../../../../brawlhallaEntities/components/icons/WeaponIcon";

type Props = {
    filter: StatisticFilterBase,
    size: string,
};

// const getTeamSize = (filter: StatisticFilterBase): string | null => {
//     if (gameType === null) {
//         return null;
//     } else if (gameType === GameType.unranked1V1 || gameType === GameType.ranked1V1) {
//         return '1 vs 1'
//     } else if (gameType === GameType.unranked2V2 || gameType === GameType.ranked2V2) {
//         return '2 vs 2'
//     }
//     throw new Error('Unexpected game type. Cant determine team size.')
// }

const getIcon = (
    weaponId: number | null,
    legendId: number | null,
    weapons: Weapon[],
    legends: Legend[],
    size: string
) => {
    if (legendId === null && weaponId === null) {
        return <LegendIcon width={size} height={size}/>
    } else if (legendId !== null) {
        const name = legends.find(x => x.id === legendId)!.name;
        return <LegendIcon width={size} height={size} name={name}></LegendIcon>
    } else {
        const name = weapons.find(x => x.id === weaponId)!.name;
        return <WeaponIcon width={size} height={size} name={name}></WeaponIcon>
    }
}

const Item = styled(Paper)(({theme}) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: 'center',
    color: theme.palette.text.secondary,
}));

export const EntitiesSetupView: FC<Props> = ({filter, size}: Props) => {
    const [weapons, legends, fetched] = useBrawlhallaEntities(true)
    if (!fetched) {
        return <CircularProgress color="inherit"/>
    }
    const playerIcon = getIcon(filter.weaponId, filter.legendId, weapons, legends, size);
    const teammateIcon = getIcon(filter.teammateWeaponId, filter.teammateLegendId, weapons, legends, size);
    const enemyIcon = getIcon(filter.enemyWeaponId, filter.enemyLegendId, weapons, legends, size);

    // if (filter.teammateLegendId === null && filter.teammateWeaponId === null) {
    //     teammateIcon = <LegendIcon width={size} height={size}/>
    // } else if (filter.teammateLegendId !== null) {
    //     const name = legends.find(x => x.id === filter.teammateLegendId)!.name;
    //     teammateIcon = <LegendIcon width={size} height={size} name={name}></LegendIcon>
    // } else {
    //     const name = weapons.find(x => x.id === filter.teammateWeaponId)!.name;
    //     teammateIcon = <WeaponIcon width={size} height={size} name={name}></WeaponIcon>
    // }


    return (
        <Stack
            direction="row"
            spacing={2}
        >
            {playerIcon}
            {teammateIcon}
            <Divider orientation="vertical" flexItem>VS</Divider>
            {enemyIcon}
        </Stack>
    );
};