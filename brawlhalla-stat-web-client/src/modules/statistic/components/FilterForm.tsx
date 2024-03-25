import React, {FC, FormEvent, useState} from 'react';
import {Button, Container, Divider, List, Paper} from "@mui/material";
import {StatisticFilterCreate} from "../types";
import {PlayerDataSelect} from "./PlayerDataSelect";
import {GameType} from "../../brawlhallaEntities/types";
import {GameTypeSelect} from "../../brawlhallaEntities/components/GameTypeSelect";
import {useRootSelector} from "../../../store";

type FilterFormProps = {
    onSubmit: (filter: StatisticFilterCreate) => void;
};

export const FilterForm: FC<FilterFormProps> = ({onSubmit}: FilterFormProps) => {
    const state = useRootSelector(state => state.statisticReducer.form);

    const [gameType, setGameType] = useState<GameType | null>(null)
    const [legendId, setLegendId] = useState<number | null>(null)
    const [weaponId, setWeaponId] = useState<number | null>(null)
    const [enemyLegendId, setEnemyLegendId] = useState<number | null>(null)
    const [enemyWeaponId, setEnemyWeaponId] = useState<number | null>(null)
    const [teammateLegendId, setTeammateLegendId] = useState<number | null>(null)
    const [teammateWeaponId, setTeammateWeaponId] = useState<number | null>(null)

    const is2v2ModeSelected = gameType !== null
        ? (gameType === GameType.ranked2V2 || gameType === GameType.unranked2V2)
        : true;

    const handleSubmit = async (event: FormEvent) => {
        event.preventDefault();

        const data: StatisticFilterCreate = {
            gameType,
            legendId,
            weaponId,
            enemyLegendId,
            enemyWeaponId,
            teammateLegendId,
            teammateWeaponId,
        }

        onSubmit(data);
    };

    return (
        <Container
            sx={{color: 'primary', display: 'flex'}}
        >
            <Paper elevation={6} sx={{py: '20px'}}>
                <Container sx={{height: '100%', display: 'flex', flexDirection: 'column', justifyContent: 'space-between'}}>
                    <Container sx={{display: 'flex', justifyContent: 'center', mb: 2}}>
                        <GameTypeSelect gameTypeChange={setGameType}/>
                    </Container>
                    <Divider>Players</Divider>
                    <Container sx={{display: 'flex', mb:'8px'}}>
                        <List sx={{display: 'flex', flexDirection: 'column', justifyContent: 'center'}} >
                            <PlayerDataSelect weaponIdChange={setWeaponId} legendIdChange={setLegendId}/>
                            {is2v2ModeSelected
                                ?
                                <>
                                    <Divider>With</Divider>
                                    <PlayerDataSelect  weaponIdChange={setTeammateWeaponId} legendIdChange={setTeammateLegendId}/>
                                </>
                                : <> </>
                            }
                        </List>
                        <Divider orientation="vertical" flexItem>VS</Divider>
                        <List sx={{display: 'flex', flexDirection: 'column', justifyContent: 'center'}} >
                            <PlayerDataSelect weaponIdChange={setEnemyWeaponId} legendIdChange={setEnemyLegendId}/>
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