import {FC, SyntheticEvent, useEffect, useState} from 'react';
import {Autocomplete, CircularProgress, createFilterOptions, InputAdornment, TextField} from "@mui/material";
import {Legend} from "../types";
import Box from "@mui/material/Box";
import {LegendIcon} from "./icons/LegendIcon";
import {WeaponIcon} from "./icons/WeaponIcon";
import {useBrawlhallaEntities} from "../hooks/useBrawlhallaEntities";

type LegendSelectProp = {
    legendChange: (legend: Legend | null) => void,
    hidden?: boolean | undefined
}
const filterOptions = createFilterOptions({
    matchFrom: 'start',
    stringify: (option: Legend) => option.name
})

export const LegendSelect: FC<LegendSelectProp> = ({legendChange, hidden}) => {
    const [, legends, fetched, tryFetch] = useBrawlhallaEntities(false);
    const isLoading = !fetched

    const [open, setOpen] = useState<boolean>(false);
    const [legend, setLegend] = useState<Legend | null>(null);
    const [inputValue, setInputValue] = useState<string>('')

    const handleChange = (event: SyntheticEvent<Element, Event>, newValue: Legend | null) => {
        setLegend(newValue);
        legendChange(newValue);
    }

    useEffect(() => {
        if (!open || fetched) {
            return;
        }

        tryFetch()
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
            loading={isLoading}
            onInputChange={(event, newInputValue) => {
                setInputValue(newInputValue);
            }}
            options={legends}
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
                                {isLoading && open
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
                                    <LegendIcon name={legend.name}/>
                                </Box>
                            </InputAdornment>
                            : null
                    }}
                />
            )}
        />
    );
};