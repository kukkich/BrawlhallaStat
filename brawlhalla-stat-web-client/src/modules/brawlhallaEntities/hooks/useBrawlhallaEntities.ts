import {useRootDispatch, useRootSelector} from "../../../store";
import {getEntitiesAction} from "../store/actions";
import { Legend, Weapon } from "../types";

export const useBrawlhallaEntities = (autoFetch: boolean): [Weapon[], Legend[], boolean, () => Promise<void>] => {
    const dispatch = useRootDispatch()
    const entities = useRootSelector(state => state.entitiesReducer)
    const fetched = entities.legends !== null && entities.weapons !== null

    const tryStartFetching = async () => {
        if (entities.isFetching || fetched) {
            return
        }
        await dispatch(getEntitiesAction())
    }

    if (!fetched && autoFetch) {
        tryStartFetching()
    }

    return [
        entities.weapons ?? [],
        entities.legends ?? [],
        fetched,
        tryStartFetching
    ]
}