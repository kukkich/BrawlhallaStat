import {FC} from 'react';
import {StatisticFilterBase} from "../../../types";
import {CircularProgress, Divider, Stack} from "@mui/material";
import {Legend, Weapon} from "../../../../brawlhallaEntities/types";
import {LegendIcon} from "../../../../brawlhallaEntities/components/icons/LegendIcon";
import {useBrawlhallaEntities} from "../../../../brawlhallaEntities/hooks/useBrawlhallaEntities";
import {WeaponIcon} from "../../../../brawlhallaEntities/components/icons/WeaponIcon";

type Props = {
    filter: StatisticFilterBase,
    size: string,
};

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

export const EntitiesSetupView: FC<Props> = ({filter, size}: Props) => {
    const [weapons, legends, fetched] = useBrawlhallaEntities(true)
    if (!fetched) {
        return <CircularProgress color="inherit"/>
    }
    const playerIcon = getIcon(filter.weaponId, filter.legendId, weapons, legends, size);
    const teammateIcon = getIcon(filter.teammateWeaponId, filter.teammateLegendId, weapons, legends, size);
    const enemyIcon = getIcon(filter.enemyWeaponId, filter.enemyLegendId, weapons, legends, size);

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