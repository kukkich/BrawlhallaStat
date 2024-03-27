import {useRootDispatch, useRootSelector} from "../../../store";
import {getEntitiesAction} from "../store/actions";

export const useBrawlhallaEntities = (autoFetch: boolean) => {
    const dispatch = useRootDispatch()
    const entities = useRootSelector(state => state.entitiesReducer)
    const fetched = entities.legends === null || entities.weapons === null

    const startFetching = () => {
        dispatch(getEntitiesAction())
    }

    if (!fetched && autoFetch) {
        startFetching()
    }

    return [entities.weapons ?? [], entities.legends ?? [], fetched, startFetching]
}