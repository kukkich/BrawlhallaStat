import React, {FC, FormEvent, useState} from 'react';
import {Button, Container, Divider, List, Paper, Select} from "@mui/material";
import {StatisticFilterCreate} from "../types";
import {PlayerDataSelect} from "./PlayerDataSelect";
import {GameType} from "../../brawlhallaEntities/types";
import {GameTypeSelect} from "../../brawlhallaEntities/components/GameTypeSelect";

type FilterFormProps = {
    onSubmit: (filter: StatisticFilterCreate) => void;
};



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
                    <Container sx={{display: 'flex', justifyContent: 'center', mb: 2}}>
                        <GameTypeSelect gameTypeChange={setGameType}/>
                    </Container>
                    <Divider>Players</Divider>
                    <Container sx={{display: 'flex'}}>
                        <List>
                            <PlayerDataSelect setWeaponId={setWeaponId} setLegendId={setLegendId}/>
                            <Divider>With</Divider>
                            <PlayerDataSelect setWeaponId={setTeammateWeaponId} setLegendId={setTeammateLegendId}/>
                        </List>
                        <Divider orientation="vertical" flexItem>VS</Divider>
                        <List>
                            <PlayerDataSelect setWeaponId={setEnemyWeaponId} setLegendId={setEnemyLegendId}/>
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