import {FC, SyntheticEvent, useEffect, useState} from 'react';
import {Autocomplete, CircularProgress, createFilterOptions, InputAdornment, TextField} from "@mui/material";
import {useRootDispatch, useRootSelector} from "../../../store";
import {getEntitiesAction} from "../store/actions";
import {Weapon} from "../types";
import Box from "@mui/material/Box";
import {WeaponIcon} from "./icons/WeaponIcon";
import {useBrawlhallaEntities} from "../hooks/useBrawlhallaEntities";

type WeaponSelectProp = {
    weaponChange: (weapon: Weapon | null) => void,
    hidden?: boolean | undefined
}
const filterOptions = createFilterOptions({
    matchFrom: 'start',
    stringify: (option: Weapon) => option.name
})

// TODO add hints. See: https://mui.com/material-ui/react-autocomplete/#hint
// TODO add text "Any" when nothing selected
export const WeaponSelect: FC<WeaponSelectProp> = ({weaponChange, hidden}) => {
    const [weapons, , fetched, tryFetch] = useBrawlhallaEntities(false);
    const isLoading = !fetched

    const [open, setOpen] = useState<boolean>(false);
    const [weapon, setWeapon] = useState<Weapon | null>(null);
    const [inputValue, setInputValue] = useState<string>('')

    const handleChange = (event: SyntheticEvent<Element, Event>, newValue: Weapon | null) => {
        setWeapon(newValue);
        weaponChange(newValue);
    }

    useEffect(() => {
        if (!open || fetched) {
            return;
        }

        tryFetch()
    }, [open,]);

    return (
        <Autocomplete
            sx={{ width: 300 }}
            hidden={hidden}
            value={weapon}
            filterOptions={filterOptions}
            isOptionEqualToValue={(option, value) => option.id === value.id}
            getOptionLabel={(option: Weapon) => option.name}
            onChange={handleChange}
            open={open}
            onOpen={() => {
                setOpen(true);
            }}
            onClose={() => {
                setOpen(false);
            }}
            inputValue={inputValue}
            loading={isLoading}
            onInputChange={(event, newInputValue) => {
                setInputValue(newInputValue);
            }}
            options={weapons}
            renderOption={(props, option) => (
                <Box component="li" sx={{'& > img': {mr: 2}}} {...props}>
                    <WeaponIcon name={option.name} width='50'/>
                    {option.name}
                </Box>
            )}
            renderInput={params => (
                <TextField {...params} label="Weapon"
                    InputProps={{
                        ...params.InputProps,
                        endAdornment: (
                            <>
                                {isLoading && open
                                    ? <CircularProgress size={20} color="inherit"/>
                                    : null
                                }
                                {params.InputProps.endAdornment}
                            </>
                        ),
                        startAdornment: weapon !== null
                            ?
                            <InputAdornment position="start" >
                                <Box sx={{
                                    '& > img': {
                                        mt: 1,
                                        height: '45px'
                                    }}}
                                >
                                    <WeaponIcon name={weapon.name}/>
                                </Box>
                            </InputAdornment>
                            : null
                    }}
                />
            )}
        />
    );
};