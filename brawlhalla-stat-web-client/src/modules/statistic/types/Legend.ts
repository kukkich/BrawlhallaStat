import {Weapon} from "./Weapon";

export interface Legend {
    Id: number;
    Name: string;
    FirstWeapon: Weapon;
    SecondWeapon: Weapon;
}
