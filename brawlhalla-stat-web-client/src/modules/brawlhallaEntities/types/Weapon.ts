import {IHaveId} from "./IHaveId";

export interface Weapon extends IHaveId<number> {
    name: string;
}