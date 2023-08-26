import {Weapon} from "./Weapon";

export interface Legend {
    Id: number;
    Name: string;
    FirstWeaponId: number;
    FirstWeapon: Weapon;
    SecondWeaponId: number;
    SecondWeapon: Weapon;
}
