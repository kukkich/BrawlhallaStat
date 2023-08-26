import {FC} from 'react';
import {Box, Grid, styled} from "@mui/material";
import LegendStatisticTable from "./components/LegendStatisticTable";
import {LegendStatistic} from "../types/LegendStatistic";

const Item = styled('div')(({theme}) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    border: '1px solid',
    borderColor: theme.palette.mode === 'dark' ? '#444d58' : '#ced7e0',
    padding: theme.spacing(1),
    borderRadius: '4px',
    textAlign: 'center',
}));

const GridTemplate: FC = () => {
    return <Box sx={{flexGrow: 1}}>
        <Grid container columnSpacing={{xs: 1, sm: 2, md: 3}}>
            <Grid item xs={2}>
                <Item>xs=8</Item>
            </Grid>
            <Grid item xs={4}>
                <Item>xs=4</Item>
            </Grid>
            <Grid item xs={6}>
                <Item>xs=4</Item>
            </Grid>
            <Grid item xs={8}>
                <Item>xs=8</Item>
            </Grid>
        </Grid>
    </Box>
}

const LegendsPage: FC = () => {

    const legendStatistics = [
        {
            Id: '1',
            LegendId: 1,
            Legend: { Id: 1, Name: 'Legend A' },
            StatisticId: '1',
            Statistic: { Wins: 10, Lost: 5, Total: 15 },
        },
        {
            Id: '2',
            LegendId: 2,
            Legend: { Id: 2, Name: 'Legend B' },
            StatisticId: '2',
            Statistic: { Wins: 7, Lost: 3, Total: 10 },
        },
        {
            Id: '3',
            LegendId: 3,
            Legend: { Id: 3, Name: 'Legend C' },
            StatisticId: '3',
            Statistic: { Wins: 15, Lost: 10, Total: 25 },
        },
        {
            Id: '4',
            LegendId: 4,
            Legend: { Id: 4, Name: 'Legend D' },
            StatisticId: '4',
            Statistic: { Wins: 3, Lost: 2, Total: 5 },
        },
        {
            Id: '5',
            LegendId: 5,
            Legend: { Id: 5, Name: 'Legend E' },
            StatisticId: '5',
            Statistic: { Wins: 8, Lost: 7, Total: 15 },
        },
        {
            Id: '6',
            LegendId: 6,
            Legend: { Id: 6, Name: 'Legend F' },
            StatisticId: '6',
            Statistic: { Wins: 20, Lost: 15, Total: 35 },
        },
        {
            Id: '7',
            LegendId: 7,
            Legend: { Id: 7, Name: 'Legend G' },
            StatisticId: '7',
            Statistic: { Wins: 12, Lost: 8, Total: 20 },
        },
        {
            Id: '8',
            LegendId: 8,
            Legend: { Id: 8, Name: 'Legend H' },
            StatisticId: '8',
            Statistic: { Wins: 6, Lost: 4, Total: 10 },
        },
        {
            Id: '9',
            LegendId: 9,
            Legend: { Id: 9, Name: 'Legend I' },
            StatisticId: '9',
            Statistic: { Wins: 18, Lost: 12, Total: 30 },
        },
        {
            Id: '10',
            LegendId: 10,
            Legend: { Id: 10, Name: 'Legend J' },
            StatisticId: '10',
            Statistic: { Wins: 5, Lost: 5, Total: 10 },
        },
    ] as LegendStatistic[];

    return (
        <>
            <Grid item container >
                <LegendStatisticTable legendStatistics={legendStatistics}/>
            </Grid>
        </>
    );
};

export default LegendsPage;