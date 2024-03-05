import {FC, SyntheticEvent, useEffect, useState} from 'react';
import {Autocomplete, CircularProgress, createFilterOptions, TextField} from "@mui/material";
import {Legend} from "../types";
import {useRootDispatch, useRootSelector} from "../../../store";
import {getEntitiesAction} from "../store/actions";

type LegendSelectProp = {
    legendChange: (legend: Legend | null) => void
}
const filterOptions = createFilterOptions({
    matchFrom: 'start',
    stringify: (option: Legend) => option.name
})

export const LegendSelect: FC<LegendSelectProp> = ({legendChange}) => {

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
                        )
                    }}
                />
            )}
        />
    );
};