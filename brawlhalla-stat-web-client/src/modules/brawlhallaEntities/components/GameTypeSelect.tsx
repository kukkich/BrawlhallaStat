import {FormControl, InputLabel, MenuItem, Select, SelectChangeEvent} from '@mui/material';
import {FC, useState} from 'react';
import {GameType} from '../types';
import Box from "@mui/material/Box";

type GameTypeSelectProp = {
    gameTypeChange: (newType: GameType | null) => void;
};

export const GameTypeSelect: FC<GameTypeSelectProp> = ({ gameTypeChange }: GameTypeSelectProp) => {
    const [gameType, setGameType] = useState<string>('');

    const handleChange = (event: SelectChangeEvent) => {
        console.log(event.target.value)
        setGameType(event.target.value)
        const value = event.target.value as number | string;
        const type = (value !== ''
            ? value
            : null) as GameType || null;
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
                    <MenuItem value={GameType.unranked1V1}>Unranked 1v1</MenuItem>
                    <MenuItem value={GameType.ranked1V1}>Ranked 1v1</MenuItem>
                    <MenuItem value={GameType.unranked2V2}>Unranked 2v2</MenuItem>
                    <MenuItem value={GameType.ranked2V2}>Ranked 2v2</MenuItem>
                </Select>
            </FormControl>
        </Box>
    );
};