import {Weapon} from "./Weapon";

export interface Legend {
    Id: number;
    name: string;
    FirstWeapon: Weapon;
    SecondWeapon: Weapon;
}
