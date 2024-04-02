import {Statistic} from "./index";
import {GameType} from "../../brawlhallaEntities/types";

export interface StatisticFilterBase {
    gameType: GameType | null,
    legendId: number | null,
    weaponId: number | null,
    enemyLegendId: number | null,
    enemyWeaponId: number | null,
    teammateLegendId: number | null,
    teammateWeaponId: number | null,
}

export interface StatisticFilterCreate extends StatisticFilterBase { }

export interface StatisticFilter extends StatisticFilterBase {
    id: string,
    createdAt: Date
}

export interface StatisticWithFilter {
    statistic: Statistic,
    filter: StatisticFilter
}

export interface PagedStatisticWithFilter {
    statisticWithFilter: StatisticWithFilter[],
    total: number
}