import React, {FC, FormEvent, useState} from 'react';
import {Box, Button, Container, Divider, List, ListItem, ListSubheader, Paper} from "@mui/material";
import {GameType, StatisticFilterCreate} from "../types";
import {WeaponSelect} from "../../brawlhallaEntities/components/WeaponSelect";
import {IHaveId} from "../../brawlhallaEntities/types/IHaveId";
import {LegendSelect} from "../../brawlhallaEntities/components/LegendSelect";
import {getTheme} from "../../theme";
import {useRootSelector} from "../../../store";
import {ThemeMode} from "../../theme/types";

type FilterFormProps = {
    onSubmit: (filter: StatisticFilterCreate) => void;
};

const getIdOrNull = (item: IHaveId<number> | null) => {
    if (item === null){
        return null;
    }
    return item.id;
}

export const FilterForm: FC<FilterFormProps> = ({onSubmit}: FilterFormProps) => {
    const [gameType, setGameType] = useState<GameType | null>(null)
    const [legendId, setLegendId] = useState<number | null>(null)
    const [weaponId, setWeaponId] = useState<number | null>(null)
    const [enemyLegendId, setEnemyLegendId] = useState<number | null>(null)
    const [enemyWeaponId, setEnemyWeaponId] = useState<number | null>(null)
    const [teammateLegendId, setTeammateLegendId] = useState<number | null>(null)
    const [teammateWeaponId, setTeammateWeaponId] = useState<number | null>(null)

    const handleSubmit = async (event: FormEvent) => {
        event.preventDefault();

        onSubmit({
            gameType,
            legendId,
            weaponId,
            enemyLegendId,
            enemyWeaponId,
            teammateLegendId,
            teammateWeaponId,
        });
    };

    return (
        <Container
            sx={{color: 'primary', display: 'flex'}}
        >
            <Paper elevation={6} sx={{py: '20px'}}
                   // style={{padding: '20px'}}
            >
                <Container>
                    <Container sx={{display: 'flex'}}>
                        <List>
                            <ListItem>
                                <WeaponSelect weaponChange={x => setWeaponId(getIdOrNull(x))}/>
                            </ListItem>
                            <ListItem>
                                <LegendSelect legendChange={x => setLegendId(getIdOrNull(x))}/>
                            </ListItem>
                            <Divider>With</Divider>
                            <ListItem>
                                <WeaponSelect weaponChange={x => setTeammateWeaponId(getIdOrNull(x))}/>
                            </ListItem>
                            <ListItem>
                                <LegendSelect legendChange={x => setTeammateLegendId(getIdOrNull(x))}/>
                            </ListItem>
                        </List>
                    <Divider orientation="vertical" flexItem>VS</Divider>
                    <List>
                        <ListItem>
                            <WeaponSelect weaponChange={x => setEnemyWeaponId(getIdOrNull(x))}/>
                        </ListItem>
                        <ListItem>
                            <LegendSelect legendChange={x => setEnemyLegendId(getIdOrNull(x))}/>
                        </ListItem>
                    </List>
                    </Container>
                    <Button
                        variant="contained"
                        onClick={handleSubmit}
                    >
                        Create
                    </Button>
                </Container>
            </Paper>
        </Container>

    );
};