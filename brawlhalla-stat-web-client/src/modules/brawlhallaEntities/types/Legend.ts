import {Weapon} from "./Weapon";
import {IHaveId} from "./IHaveId";

export interface Legend extends IHaveId<number>{
    name: string;
    firstWeapon: Weapon;
    secondWeapon: Weapon;
}
