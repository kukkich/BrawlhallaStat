import $api from "../../../api/axios";
import {GetEntitiesResult} from "./getEntitiesResult";
import {Legend} from "../types";
import {Weapon} from "../types";
import {getDetailsFromApiException} from "../../../api/tools/getDetailsFromApiException";

export default class entitiesService {
    static async getEntities(): Promise<GetEntitiesResult> {
        try {
            const legendsResponse = await $api.get<Legend[]>('/entities/legends');
            const weaponsResponse = await $api.get<Weapon[]>('/entities/weapons');
            return {
                legends: legendsResponse.data,
                weapons: weaponsResponse.data
            }
        } catch (e) {
            throw getDetailsFromApiException(e)
        }
    }
}