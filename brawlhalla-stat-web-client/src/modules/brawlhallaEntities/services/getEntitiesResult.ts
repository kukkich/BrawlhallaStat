import {Legend} from "../types/Legend";
import {Weapon} from "../types/Weapon";

export interface GetEntitiesResult {
    legends: Legend[] | null
    weapons: Weapon[] | null
}