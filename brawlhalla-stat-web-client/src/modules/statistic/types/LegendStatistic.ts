import { Legend } from "./Legend";
import { Statistic } from "./Statistic";

export interface LegendStatistic {
    Id: string;
    LegendId: number;
    Legend: Legend;
    StatisticId: string;
    Statistic: Statistic;
}