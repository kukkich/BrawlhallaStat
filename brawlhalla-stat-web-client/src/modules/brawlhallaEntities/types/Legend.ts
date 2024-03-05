import {Weapon} from "./Weapon";

export interface Legend {
    id: number;
    name: string;
    firstWeapon: Weapon;
    secondWeapon: Weapon;
}
