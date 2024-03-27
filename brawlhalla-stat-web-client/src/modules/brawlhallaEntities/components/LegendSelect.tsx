import {FC, SyntheticEvent, useEffect, useState} from 'react';
import {Autocomplete, CircularProgress, createFilterOptions, InputAdornment, TextField} from "@mui/material";
import {Legend} from "../types";
import {useRootDispatch, useRootSelector} from "../../../store";
import {getEntitiesAction} from "../store/actions";
import Box from "@mui/material/Box";
import {LegendIcon} from "./icons/LegendIcon";
import {WeaponIcon} from "./icons/WeaponIcon";

type LegendSelectProp = {
    legendChange: (legend: Legend | null) => void,
    hidden?: boolean | undefined
}
const filterOptions = createFilterOptions({
    matchFrom: 'start',
    stringify: (option: Legend) => option.name
})

export const LegendSelect: FC<LegendSelectProp> = ({legendChange, hidden}) => {

    const dispatch = useRootDispatch();
    const entitiesState = useRootSelector(state => state.entitiesReducer);

    const [open, setOpen] = useState<boolean>(false);
    const [legend, setLegend] = useState<Legend | null>(null);
    const [inputValue, setInputValue] = useState<string>('')

    const handleChange = (event: SyntheticEvent<Element, Event>, newValue: Legend | null) => {
        setLegend(newValue);
        legendChange(newValue);
    }

    useEffect(() => {
        if (!open || entitiesState.isFetching) {
            return;
        }

        (async () => {
            if (entitiesState.legends === null && !entitiesState.isFetching) {
                await dispatch(getEntitiesAction());
            }
        })();
    }, [open]);

    return (
        <Autocomplete
            sx={{ width: 300 }}
            hidden={hidden}
            value={legend}
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
            options={entitiesState.legends === null ? [] : entitiesState.legends}
            renderOption={(props, option) => (
                <Box component="li"
                     sx={{'& > img': {mr: 2}}}
                     {...props}>
                    <LegendIcon name={option.name} width='50'/>
                    <WeaponIcon name={option.firstWeapon.name} width='40'/>
                    <WeaponIcon name={option.secondWeapon.name} width='40'/>
                    {option.name}
                </Box>
            )}
            renderInput={params => (
                <TextField {...params} label="Legend"
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
                        ),
                        startAdornment: legend !== null
                            ?
                            <InputAdornment position="start" >
                                <Box sx={{
                                    '& > img': {
                                        mt: 1,
                                        height: '45px'
                                    }}}
                                >
                                    <img loading="lazy"
                                         src={process.env.PUBLIC_URL + `/heroes/${legend.name}.png`}
                                         alt=""
                                    />
                                </Box>
                            </InputAdornment>
                            : null
                    }}
                />
            )}
        />
    );
};