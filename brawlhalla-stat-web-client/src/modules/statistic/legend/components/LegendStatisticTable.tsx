import {FC} from 'react';
import {
    Grid,
    Table,
    TableHead,
    TableBody,
    TableRow,
    TableCell,
    Typography,
} from '@mui/material';
import {LegendStatistic} from "../../types/LegendStatistic";

interface LegendStatisticTableProps {
    legendStatistics: LegendStatistic[];
}


const LegendStatisticTable: FC<LegendStatisticTableProps> = ({ legendStatistics }) => {
    return (
        <Grid container justifyContent="center">
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Legend</TableCell>
                        <TableCell>Win Rate</TableCell>
                        <TableCell>Wins</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {legendStatistics.map((legendStat) => (
                        <TableRow key={legendStat.Id}>
                            <TableCell>{legendStat.Legend.Name}</TableCell>
                            <TableCell>
                                {((legendStat.Statistic.Wins / legendStat.Statistic.Total) * 100).toFixed(2)}%
                            </TableCell>
                            <TableCell>
                                {legendStat.Statistic.Wins}/{legendStat.Statistic.Total}
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </Grid>
    );
};

export default LegendStatisticTable;
