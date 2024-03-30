import {FC, useEffect, useState} from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import AddIcon from '@mui/icons-material/Add';
import {
    GridRowsProp,
    GridRowModesModel,
    GridRowModes,
    DataGrid,
    GridColDef,
    GridToolbarContainer,
    GridActionsCellItem,
    GridRowId,
    GridSlots,
} from '@mui/x-data-grid';
import {
    randomCreatedDate,
    randomTraderName,
    randomId,
    randomArrayItem,
} from '@mui/x-data-grid-generator/services/random-generator';
import { Delete } from '@mui/icons-material';
import {useRootDispatch, useRootSelector} from "../../../../store";
import {StatisticWithFilter} from "../../types";
import {deleteFilter, fetchStatistics} from "../../store/actions";
import {FilterView} from "./Views/FilterView";
import {CircularProgress} from "@mui/material";

interface EditToolbarProps {
    setRows: (newRows: (oldRows: GridRowsProp) => GridRowsProp) => void;
    setRowModesModel: (
        newModel: (oldModel: GridRowModesModel) => GridRowModesModel,
    ) => void;
}

function EditToolbar(props: EditToolbarProps) {
    const handleClick = () => {
    };

    return (
        <GridToolbarContainer>
            <Button color="primary" startIcon={<AddIcon />} onClick={handleClick}>
                Add record
            </Button>
        </GridToolbarContainer>
    );
}

export const StatisticTable: FC = () => {
    const dispatch = useRootDispatch();
    useEffect(() => {
        dispatch(fetchStatistics())
    })

    const statisticState = useRootSelector(x => x.statisticReducer)
    const statRows = statisticState.statistics

    const handleDeleteClick = (id: GridRowId) => () => {
        dispatch(deleteFilter(id as string))
    };

    const columns: GridColDef[] = [
        {
            field: 'actions',
            type: 'actions',
            headerName: 'Actions',
            width: 100,
            getActions: ({ id }) => {
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
        { field: 'name', headerName: 'Name', width: 180},
        {
            field: 'age',
            headerName: 'Age',
            width: 80,
            align: 'left',
            headerAlign: 'left',
        },
        { field: 'joinDate', headerName: 'Join date', width: 180},
        { field: 'role', headerName: 'Department', width: 220},
    ];
    const statColumns: GridColDef[] = [
        {
            field: 'actions',
            type: 'actions',
            headerName: 'Actions',
            width: 100,
            getActions: ({ id }) => {
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
            width: 300,
            renderCell: (params) => <FilterView filter={params.value}/>,
        },
        // {
        //     field: 'id',
        //     headerName: 'Id',
        //     width: 380,
        //     valueGetter: (_, item: StatisticWithFilter) => item.filter.id,
        // },
        {
            field: 'wins',
            headerName: 'Wins',
            valueGetter: (_, item: StatisticWithFilter) => item.statistic.wins,
        },
    ];

    return (
        <Box sx={{ height: 800, width: '100%',
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
    );
};