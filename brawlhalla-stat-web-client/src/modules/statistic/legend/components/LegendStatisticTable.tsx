import React from 'react';
import {DataGrid, GridColDef, GridValueGetterParams} from '@mui/x-data-grid';
import {LegendStatistic} from '../../types/LegendStatistic';

interface LegendStatisticTableProps {
    legendStatistics: LegendStatistic[];
}

const columns: GridColDef[] = [
    {
        field: 'legend',
        headerName: 'Legend',
        flex: 1,
        valueGetter: (params: GridValueGetterParams) => params.row.Legend.Name
    },
    {
        field: 'winRate', headerName: 'Win Rate', flex: 1, valueGetter: (params) => {
            const winRate = (params.row.Statistic.Wins / params.row.Statistic.Total) * 100;
            return `${winRate.toFixed(2)}%`;
        }
    },
    {
        field: 'wins',
        headerName: 'Wins',
        flex: 1,
        valueGetter: (params) => `${params.row.Statistic.Wins}/${params.row.Statistic.Total}`
    },
];

const LegendStatisticTable: React.FC<LegendStatisticTableProps> = ({legendStatistics}) => {

    return (
        <DataGrid
            rows={legendStatistics}
            columns={columns}
            initialState={{
                pagination: {
                    paginationModel: {page: 0, pageSize: 5},
                },
            }}
            pageSizeOptions={[5, 10]}
            getRowId={(row: LegendStatistic) => row.Id}
            autoHeight
        />
    );
};

export default LegendStatisticTable;
