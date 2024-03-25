import React, {FC, FormEvent, useState} from 'react';
import {Button, Container, Divider, List, Paper} from "@mui/material";
import {StatisticFilterCreate} from "../types";
import {PlayerDataSelect} from "./PlayerDataSelect";
import {GameType} from "../../brawlhallaEntities/types";
import {GameTypeSelect} from "../../brawlhallaEntities/components/GameTypeSelect";
import {useRootSelector} from "../../../store";
import {statisticActions} from "../store/reducer";
import {useDispatch} from "react-redux";

type FilterFormProps = {
    onSubmit: (filter: StatisticFilterCreate) => void;
};

export const FilterForm: FC<FilterFormProps> = ({onSubmit}: FilterFormProps) => {
    const dispatch = useDispatch();
    const state = useRootSelector(state => state.statisticReducer);
    const data = state.form.data;

    const [gameType, setGameType] = [
        data.gameType,
        (x: GameType | null) => dispatch(statisticActions.setFormState({...data, gameType: x}))
    ]
    const [legendId, setLegendId] = [
        data.legendId,
        (x: number | null) => statisticActions.setFormState({...data, legendId: x})
    ]
    const [weaponId, setWeaponId] = [
        data.weaponId,
        (x: number | null) => statisticActions.setFormState({...data, weaponId: x})
    ]
    const [enemyLegendId, setEnemyLegendId] = [
        data.enemyLegendId,
        (x: number | null) => statisticActions.setFormState({...data, enemyLegendId: x})
    ]
    const [enemyWeaponId, setEnemyWeaponId] = [
        data.enemyWeaponId,
        (x: number | null) => statisticActions.setFormState({...data, enemyWeaponId: x})
    ]
    const [teammateLegendId, setTeammateLegendId] = [
        data.teammateLegendId,
        (x: number | null) => statisticActions.setFormState({...data, teammateLegendId: x})
    ]
    const [teammateWeaponId, setTeammateWeaponId] = [
        data.teammateWeaponId,
        (x: number | null) => statisticActions.setFormState({...data, teammateWeaponId: x})
    ]

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