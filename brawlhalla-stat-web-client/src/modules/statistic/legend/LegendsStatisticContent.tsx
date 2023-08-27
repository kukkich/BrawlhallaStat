import {FC} from 'react';
import LegendStatisticTable from "./components/LegendStatisticTable";
import {LegendStatistic} from "../types/LegendStatistic";
import {Paper, TableContainer} from "@mui/material";


function generateRandomLegendStatistic(id: number): LegendStatistic {
    const legends: string[] = ['Legend A', 'Legend B', 'Legend C', 'Legend D', 'Legend E'];
    const wins: number = Math.floor(Math.random() * 20) + 1;
    const lost: number = Math.floor(Math.random() * 10) + 1;
    const total: number = wins + lost;

    return {
        Id: id.toString(),
        Legend: { Id: id, Name: legends[id % legends.length]},
        StatisticId: id.toString(),
        Statistic: { Wins: wins, Lost: lost, Total: total },
    } as LegendStatistic;
}

const legendStatistics: LegendStatistic[] = [];
for (let i = 1; i <= 40; i++) {
    legendStatistics.push(generateRandomLegendStatistic(i));
}
const LegendsStatisticContent: FC = () => {

    return (
        <TableContainer component={Paper}>
            <LegendStatisticTable legendStatistics={legendStatistics}/>
        </TableContainer>
    );
};

export default LegendsStatisticContent;