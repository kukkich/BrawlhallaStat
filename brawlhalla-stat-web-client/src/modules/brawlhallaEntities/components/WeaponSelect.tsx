import {FC, SyntheticEvent, useEffect, useState} from 'react';
import {Autocomplete, CircularProgress, createFilterOptions, TextField} from "@mui/material";
import {useRootDispatch, useRootSelector} from "../../../store";
import {getEntitiesAction} from "../store/actions";
import {Weapon} from "../types";

type WeaponSelectProp = {
    weaponChange: (weapon: Weapon | null) => void
}
const filterOptions = createFilterOptions({
    matchFrom: 'start',
    stringify: (option: Weapon) => option.name
})

export const WeaponSelect: FC<WeaponSelectProp> = ({weaponChange}) => {

    const dispatch = useRootDispatch();
    const entitiesState = useRootSelector(state => state.entitiesReducer);

    const [open, setOpen] = useState<boolean>(false);
    const [weapon, setWeapon] = useState<Weapon | null>(null);
    const [inputValue, setInputValue] = useState<string>('')

    const handleChange = (event: SyntheticEvent<Element, Event>, newValue: Weapon | null) => {
        setWeapon(newValue);
        weaponChange(newValue);
    }

    useEffect(() => {
        if (!open || entitiesState.isFetching) {
            return;
        }

        (async () => {
            if (entitiesState.weapons === null && !entitiesState.isFetching) {
                await dispatch(getEntitiesAction());
            }
        })();
    }, [open]);

    return (
        <Autocomplete
            sx={{ width: 300 }}
            value={weapon}
            filterOptions={filterOptions}
            isOptionEqualToValue={(option, value) => option.id === value.id}
            getOptionLabel={option => option.name}
            onChange={handleChange}
            open={open}
            onOpen={() => {
                setOpen(true);
            }}
            onClose={() => {
                setOpen(false);
            }}
            inputValue={inputValue}
            loading={entitiesState.isFetching}
            onInputChange={(event, newInputValue) => {
                setInputValue(newInputValue);
            }}
            options={entitiesState.weapons === null ? [] : entitiesState.weapons}
            renderInput={params => (
                <TextField {...params} label="Weapon"
                           InputProps={{
                               ...params.InputProps,
                               endAdornment: (
                                   <>
                                       {entitiesState.isFetching && open
                                           ? <CircularProgress size={20} color="inherit"/>
                                           : null
                                       }
                                       {params.InputProps.endAdornment}
                                   </>
                               )
                           }}
                />
            )}
        />
    );
};