import {FC, useCallback, useEffect, useState} from 'react';
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
    GridSlots, GridRowSpacingParams,
} from '@mui/x-data-grid';
import {
    randomCreatedDate,
    randomTraderName,
    randomId,
    randomArrayItem,
} from '@mui/x-data-grid-generator/services/random-generator';
import { Delete } from '@mui/icons-material';
import {useRootDispatch, useRootSelector} from "../../../../store";
import {StatisticFilter, StatisticWithFilter} from "../../types";
import {fetchStatistics} from "../../store/actions";
import {FilterView} from "./FilterView";

const roles = ['Market', 'Finance', 'Development'];
const randomRole = () => {
    return randomArrayItem(roles);
};

const initialRows: GridRowsProp = [
    {
        id: randomId(),
        name: randomTraderName(),
        age: 25,
        joinDate: randomCreatedDate(),
        role: randomRole(),
    },
    {
        id: randomId(),
        name: randomTraderName(),
        age: 36,
        joinDate: randomCreatedDate(),
        role: randomRole(),
    },
    {
        id: randomId(),
        name: randomTraderName(),
        age: 19,
        joinDate: randomCreatedDate(),
        role: randomRole(),
    },
    {
        id: randomId(),
        name: randomTraderName(),
        age: 28,
        joinDate: randomCreatedDate(),
        role: randomRole(),
    },
    {
        id: randomId(),
        name: randomTraderName(),
        age: 23,
        joinDate: randomCreatedDate(),
        role: randomRole(),
    },
];

interface EditToolbarProps {
    setRows: (newRows: (oldRows: GridRowsProp) => GridRowsProp) => void;
    setRowModesModel: (
        newModel: (oldModel: GridRowModesModel) => GridRowModesModel,
    ) => void;
}

function EditToolbar(props: EditToolbarProps) {
    const { setRows, setRowModesModel } = props;

    const handleClick = () => {
        const id = randomId();
        setRows((oldRows) => [...oldRows, { id, name: '', age: '', isNew: true }]);
        setRowModesModel((oldModel) => ({
            ...oldModel,
            [id]: { mode: GridRowModes.Edit, fieldToFocus: 'name' },
        }));
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
    }, [])
    const statRows = useRootSelector(x => x.statisticReducer.statistics)

    const [rows, setRows] = useState(initialRows);

    const handleDeleteClick = (id: GridRowId) => () => {
        setRows(rows.filter((row) => row.id !== id));
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
                        icon={<Delete/>}
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
            field: 'filter',
            headerName: 'Filter',
            width: 450,
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
                slotProps={{
                    toolbar: { setRows },
                }}
            />
        </Box>
    );
};