import {FC, useEffect, useState} from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import AddIcon from '@mui/icons-material/Add';
import {
    DataGrid,
    GridActionsCellItem,
    GridColDef,
    GridRowId,
    GridRowModesModel,
    GridRowsProp,
    GridSlots,
    GridToolbarContainer,
} from '@mui/x-data-grid';
import {Delete} from '@mui/icons-material';
import {useRootDispatch, useRootSelector} from "../../../../store";
import {StatisticWithFilter} from "../../types";
import {deleteFilter, fetchStatistics} from "../../store/actions";
import {FilterView} from "./Views/FilterView";
import {CircularProgress, Fade} from "@mui/material";
import {ModalFilterForm} from "../form/ModalFilterForm";

interface EditToolbarProps {
    setRows: (newRows: (oldRows: GridRowsProp) => GridRowsProp) => void;
    setRowModesModel: (
        newModel: (oldModel: GridRowModesModel) => GridRowModesModel,
    ) => void;
}

function EditToolbar(props: EditToolbarProps) {
    const [open, setOpen] = useState<boolean>(false)

    const handleClick = () => {
        setOpen(true)
    };

    return (
        <>
            <ModalFilterForm
                open={open}
                onSubmit={(filter) => setOpen(false)}
                onClose={() => setOpen(false)}
            />
            <GridToolbarContainer>
                <Button color="primary" startIcon={<AddIcon/>} onClick={handleClick}>
                    Add filter
                </Button>
            </GridToolbarContainer>
        </>
    );
}
const currencyFormatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
});

export const StatisticTable: FC = () => {
    const [faded, setFaded] = useState(false);

    const dispatch = useRootDispatch();
    useEffect(() => {
        setFaded(true)
        dispatch(fetchStatistics())
    }, [dispatch])

    const statisticState = useRootSelector(x => x.statisticReducer)
    const statRows = statisticState.statistics

    const handleDeleteClick = (id: GridRowId) => () => {
        dispatch(deleteFilter(id as string))
    };

    const statColumns: GridColDef[] = [
        {
            field: 'actions',
            type: 'actions',
            headerName: 'Actions',
            width: 100,
            getActions: ({id}) => {
                return [
                    <GridActionsCellItem
                        icon={statisticState.removingFilterId !== id
                            ? <Delete/>
                            : <CircularProgress size={20} color="inherit"/>
                        }
                        label="Delete"
                        onClick={handleDeleteClick(id)}
                        color="error"
                    />,
                ];
            },
        },
        {
            field: 'filter',
            headerName: 'Filter',
            sortable: false,
            filterable: false,
            width: 300,
            renderCell: (params) => <FilterView filter={params.value}/>,
        },
        {
            field: 'win rate',
            headerName: 'Win rate',
            type: 'number',
            width: 140,
            valueGetter: (_, item: StatisticWithFilter) => item.statistic.wins * 100 / item.statistic.total,
            // @ts-ignore
            valueFormatter: (value) => parseFloat(value).toFixed(2)+"%",
        },
        {
            field: 'win/lost',
            headerName: 'Win/Lost',
            type: 'string',
            sortable: false,
            filterable: false,
            valueGetter: (_, item: StatisticWithFilter) => `${item.statistic.wins}/${item.statistic.defeats}`,
        },
        {
            field: 'games',
            headerName: 'Total Games',
            valueGetter: (_, item: StatisticWithFilter) => item.statistic.total
        },
    ];

    return (
        <Fade in={faded} timeout={700}>
            <Box sx={{
                height: 800, width: '100%',
                '& .actions': {
                    color: 'text.secondary',
                },
                '& .textPrimary': {
                    color: 'text.primary',
                },
            }}
            >
                <DataGrid
                    columns={statColumns}
                    rows={statRows}
                    sx={{
                        '&.MuiDataGrid-root--densityCompact .MuiDataGrid-cell': {
                            py: 2,
                        },
                        '&.MuiDataGrid-root--densityStandard .MuiDataGrid-cell': {
                            py: '15px',
                        },
                        '&.MuiDataGrid-root--densityComfortable .MuiDataGrid-cell': {
                            py: '15px',
                        },
                    }}
                    getEstimatedRowHeight={() => 100}
                    getRowHeight={() => 'auto'}
                    getRowId={(value) => value.filter.id}
                    editMode="row"
                    slots={{
                        toolbar: EditToolbar as GridSlots['toolbar'],
                    }}
                />
            </Box>
        </Fade>
    );
};
