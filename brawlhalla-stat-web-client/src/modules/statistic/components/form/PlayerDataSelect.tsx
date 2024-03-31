import {FC, useState} from 'react';
import {ListItem} from "@mui/material";
import {WeaponSelect} from "../../../brawlhallaEntities/components/WeaponSelect";
import {LegendSelect} from "../../../brawlhallaEntities/components/LegendSelect";
import {IHaveId} from "../../../brawlhallaEntities/types/IHaveId";
import {Legend, Weapon} from "../../../brawlhallaEntities/types";

type PlayerDataSelectProps = {
    weaponIdChange: (id: number | null) => void,
    legendIdChange: (id: number| null) => void,
};

const getIdOrNull = (item: IHaveId<number> | null) => {
    if (item === null){
        return null;
    }
    return item.id;
}

export const PlayerDataSelect: FC<PlayerDataSelectProps> = ({weaponIdChange, legendIdChange}: PlayerDataSelectProps) => {

    const [weaponId, setWeaponId] = useState<number | null>(null)
    const weaponSelected = weaponId !== null;
    const [legendId, setLegendId] = useState<number | null>(null)
    const legendSelected = legendId !== null;

    const handleWeaponChange = (weapon: Weapon | null) => {
        const newWeaponId = getIdOrNull(weapon)
        setWeaponId(newWeaponId)
        weaponIdChange(newWeaponId)
    }

    const handleLegendChange = (legend: Legend | null) => {
        const newLegendId = getIdOrNull(legend)
        setLegendId(newLegendId)
        legendIdChange(newLegendId)
    }

    return (
        <>
            {!legendSelected
                ?
                <ListItem>
                    <WeaponSelect hidden={legendSelected} weaponChange={handleWeaponChange}/>
                </ListItem>
                : <></>
            }
            {!weaponSelected
                ?
                <ListItem>
                    <LegendSelect hidden={weaponSelected} legendChange={handleLegendChange}/>
                </ListItem>
                : <></>
            }
        </>
    );
};