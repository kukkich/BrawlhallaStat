import $api from "../../../api/axios";
import {GetEntitiesResult} from "./getEntitiesResult";
import {Legend} from "../types/Legend";
import {Weapon} from "../types/Weapon";

export default class entitiesService {
    static async getEntities(): Promise<GetEntitiesResult> {
        const legendsResponse = await $api.get<Legend[]>('/entities/legends');
        const weaponsResponse = await $api.get<Weapon[]>('/entities/weapons');
        return {
            legends: legendsResponse.data,
            weapons: weaponsResponse.data
        }
    }
}