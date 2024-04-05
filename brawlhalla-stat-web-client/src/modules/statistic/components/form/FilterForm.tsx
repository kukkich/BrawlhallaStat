import {FC, FormEvent} from 'react';
import {Button, CircularProgress, Container, Divider, LinearProgress, List, Paper} from "@mui/material";
import {StatisticFilterCreate} from "../../types";
import {PlayerDataSelect} from "./PlayerDataSelect";
import {GameType} from "../../../brawlhallaEntities/types";
import {GameTypeSelect} from "../../../brawlhallaEntities/components/GameTypeSelect";
import {useRootDispatch, useRootSelector} from "../../../../store";
import {statisticActions} from "../../store/reducer";
import {submitForm} from "../../store/actions";
import {LoginStatus} from "../../../authentication/store/state";

type FilterFormProps = {
    onSubmit: (filter: StatisticFilterCreate) => void;
};

const teammateFieldsAvailable = (gameType: GameType | null) => {
    return gameType !== null
        ? (gameType === GameType.ranked2V2 || gameType === GameType.unranked2V2)
        : true
}

export const FilterForm: FC<FilterFormProps> = ({onSubmit}: FilterFormProps) => {
    const dispatch = useRootDispatch();
    const state = useRootSelector(state => state.statisticReducer);
    const data = state.form.data;

    const handleSubmit = async (event: FormEvent) => {
        event.preventDefault();

        await dispatch(submitForm());

        onSubmit(data);
    };

    const [gameType, setGameType] = [
        data.gameType,
        (newGameType: GameType | null) => {
            if (!teammateFieldsAvailable(newGameType)){
                dispatch(statisticActions.setFormState({
                    ...data,
                    teammateLegendId: null,
                    teammateWeaponId: null,
                    gameType: newGameType
                }))
            } else {
                dispatch(statisticActions.setFormState({...data, gameType: newGameType}))
            }
        }
    ]
    const setLegendId = (x: number | null) =>
        dispatch(statisticActions.setFormState({...data, legendId: x}))
    const setWeaponId = (x: number | null) =>
        dispatch(statisticActions.setFormState({...data, weaponId: x}))
    const setEnemyLegendId = (x: number | null) =>
        dispatch(statisticActions.setFormState({...data, enemyLegendId: x}))
    const setEnemyWeaponId = (x: number | null) =>
        dispatch(statisticActions.setFormState({...data, enemyWeaponId: x}))
    const setTeammateLegendId = (x: number | null) =>
        dispatch(statisticActions.setFormState({...data, teammateLegendId: x}))
    const setTeammateWeaponId = (x: number | null) =>
        dispatch(statisticActions.setFormState({...data, teammateWeaponId: x}))

    const showTeammateFields = teammateFieldsAvailable(gameType);

    return (
        <Container
            sx={{color: 'primary', display: 'flex'}}
        >
            <Paper elevation={6}>
                {state.form.isFetching
                    ? <LinearProgress />
                    : null}
                <Container sx={{py: '20px', height: '100%', display: 'flex', flexDirection: 'column', justifyContent: 'space-between'}}>
                    <Container sx={{display: 'flex', justifyContent: 'center', mb: 2}}>
                        <GameTypeSelect gameTypeChange={setGameType}/>
                    </Container>
                    <Divider>Players</Divider>
                    <Container sx={{display: 'flex', mb:'8px'}}>
                        <List sx={{display: 'flex', flexDirection: 'column', justifyContent: 'center'}} >
                            <PlayerDataSelect weaponIdChange={setWeaponId} legendIdChange={setLegendId}/>
                            {showTeammateFields
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
                        disabled={state.form.isFetching}
                        endIcon={state.form.isFetching
                            ? <CircularProgress size={20} color="inherit"/>
                            : null}
                    >
                        Create
                    </Button>
                </Container>
            </Paper>
        </Container>
    );
};