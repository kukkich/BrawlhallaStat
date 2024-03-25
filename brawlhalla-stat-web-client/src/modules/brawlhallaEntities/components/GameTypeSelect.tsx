import {FormControl, InputLabel, MenuItem, Select, SelectChangeEvent} from '@mui/material';
import {FC, useState} from 'react';
import {GameType} from '../types';
import Box from "@mui/material/Box";

type GameTypeSelectProp = {
    gameTypeChange: (newType: GameType | null) => void;
};

const gameTypesViewMap = new Map<string, GameType | null>();
gameTypesViewMap.set('', null)
gameTypesViewMap.set("Unranked 1v1", GameType.unranked1V1)
gameTypesViewMap.set("Ranked 1v1", GameType.ranked1V1)
gameTypesViewMap.set("Unranked 2v2", GameType.unranked2V2);
gameTypesViewMap.set("Ranked 2v2", GameType.ranked2V2)

export const GameTypeSelect: FC<GameTypeSelectProp> = ({ gameTypeChange }: GameTypeSelectProp) => {
    const [gameType, setGameType] = useState<string>('');

    const handleChange = (event: SelectChangeEvent) => {
        setGameType(event.target.value)

        const type = gameTypesViewMap.get(event.target.value) as GameType | null;
        gameTypeChange(type)
    };

    return (
        <Box sx={{ minWidth: 120 }}>
            <FormControl fullWidth>
                <InputLabel id="label">Game type</InputLabel>
                <Select
                    label="Game type"
                    labelId="label"
                    value={gameType}
                    onChange={handleChange}
                >
                    <MenuItem value={''}>Any</MenuItem>
                    <MenuItem value={'Unranked 1v1'}>Unranked 1v1</MenuItem>
                    <MenuItem value={'Ranked 1v1'}>Ranked 1v1</MenuItem>
                    <MenuItem value={'Unranked 2v2'}>Unranked 2v2</MenuItem>
                    <MenuItem value={'Ranked 2v2'}>Ranked 2v2</MenuItem>
                </Select>
            </FormControl>
        </Box>
    );
};