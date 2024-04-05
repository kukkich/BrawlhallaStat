import {FC, useEffect, useState} from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import AddIcon from '@mui/icons-material/Add';
import {DataGrid, GridActionsCellItem, GridColDef, GridRowId, GridSlots, GridToolbarContainer,} from '@mui/x-data-grid';
import {Delete} from '@mui/icons-material';
import {useRootDispatch, useRootSelector} from "../../../../store";
import {StatisticWithFilter} from "../../types";
import {deleteFilter, fetchPagedStatistics} from "../../store/actions";
import {FilterView} from "./Views/FilterView";
import {CircularProgress, Fade} from "@mui/material";
import {ModalFilterForm} from "../form/ModalFilterForm";
import {CircularProgressLabeledGradient} from "../../../UI/components/CircularProgressLabeledGradient";
import {statisticActions} from "../../store/reducer";

function EditToolbar() {
    const formState = useRootSelector(state => state.statisticReducer.form)
    const dispatch = useRootDispatch();

    const handleClick = () => {
        dispatch(statisticActions.openForm())
    };

    return (
        <>
            <ModalFilterForm
                open={formState.visible}
                onSubmit={() => {}}
                onClose={() => {
                    dispatch(statisticActions.closeForm())
                }}
            />
            <GridToolbarContainer>
                <Button color="primary" startIcon={<AddIcon/>} onClick={handleClick}>
                    Add filter
                </Button>
            </GridToolbarContainer>
        </>
    );
}

export const StatisticTable: FC = () => {
    const [faded, setFaded] = useState(false);

    const statisticState = useRootSelector(x => x.statisticReducer)
    const statRows = statisticState.statistics
    const pagination = statisticState.pagination

    const handlePaginationModelChange = (paginationModel: {page: number, pageSize: number}) => {
        dispatch(statisticActions.setPagination({...paginationModel}))
        dispatch(fetchPagedStatistics(paginationModel.page, paginationModel.pageSize))
    }

    const dispatch = useRootDispatch();
    useEffect(() => {
        setFaded(true)
        dispatch(fetchPagedStatistics(pagination.page, pagination.pageSize))
    }, [dispatch])

    const handleDeleteClick = (id: GridRowId) => () => {
        dispatch(deleteFilter(id as string))
    };

    const statColumns: GridColDef[] = [
        {
            field: 'actions',
            type: 'actions',
            headerName: 'Actions',
            headerAlign: 'center',
            align: 'center',
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
            headerAlign: 'center',
            align: 'center',
            width: 300,
            renderCell: (params) => <FilterView filter={params.value}/>
        },
        {
            field: 'win rate',
            headerName: 'Win rate',
            headerAlign: 'center',
            align: 'center',
            type: 'number',
            width: 140,
            renderCell: (params) => <CircularProgressLabeledGradient value={params.value}/>,
            valueGetter: (_, item: StatisticWithFilter) => item.statistic.wins * 100 / item.statistic.total,
            // @ts-ignore
            // valueFormatter: (value) => parseFloat(value).toFixed(2)+"%",
        },
        {
            field: 'win/lost',
            headerName: 'Win/Lost',
            type: 'string',
            headerAlign: 'center',
            align: 'center',
            sortable: false,
            filterable: false,
            valueGetter: (_, item: StatisticWithFilter) => `${item.statistic.wins}/${item.statistic.defeats}`,
        },
        {
            field: 'games',
            headerName: 'Total Games',
            headerAlign: 'center',
            align: 'center',
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
                    // editMode="row"
                    loading={statisticState.isFetching}
                    rowCount={statisticState.totalStatistics}
                    pageSizeOptions={[5, 10, 15, 20]}
                    paginationModel={pagination}
                    onPaginationModelChange={handlePaginationModelChange}
                    paginationMode='server'
                    slots={{
                        toolbar: EditToolbar as GridSlots['toolbar'],
                    }}

                />
            </Box>
        </Fade>
    );
};
