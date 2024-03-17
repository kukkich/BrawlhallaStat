import {Statistic} from "./index";

export enum GameType {
    unranked1V1,
    unranked2V2,
    ranked1V1,
    ranked2V2,
}



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
