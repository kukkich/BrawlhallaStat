import {Legend} from "../types/Legend";
import {Weapon} from "../types/Weapon";

export interface BrawlhallaEntitiesState {
    legends: Legend[] | null,
    weapons: Weapon[] | null,
    isFetching: boolean,
    errors: string[]
}