import {FC} from 'react';
import {ListItem} from "@mui/material";
import {WeaponSelect} from "../../brawlhallaEntities/components/WeaponSelect";
import {LegendSelect} from "../../brawlhallaEntities/components/LegendSelect";
import {IHaveId} from "../../brawlhallaEntities/types/IHaveId";

type PlayerDataSelectProps = {
    setWeaponId: (id: number | null) => void,
    setLegendId: (id: number| null) => void,
};

const getIdOrNull = (item: IHaveId<number> | null) => {
    if (item === null){
        return null;
    }
    return item.id;
}

export const PlayerDataSelect: FC<PlayerDataSelectProps> = ({setWeaponId, setLegendId}: PlayerDataSelectProps) => {
    return (
        <>
            <ListItem>
                <WeaponSelect weaponChange={x => setWeaponId(getIdOrNull(x))}/>
            </ListItem>
            <ListItem>
            <LegendSelect legendChange={x => setLegendId(getIdOrNull(x))}/>
            </ListItem>
        </>
    );
};