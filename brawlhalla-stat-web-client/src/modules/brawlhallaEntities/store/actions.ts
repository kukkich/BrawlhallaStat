import {AppDispatch} from "../../../store";
import {entitiesActions} from "./reducer";
import entitiesService from "../services/entitiesService";

export const requestEntitiesAction = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(entitiesActions.getEntitiesStart())
        const response = await entitiesService.getEntities();
        dispatch(entitiesActions.getEntitiesSuccess(response.data));
    } catch (e: any){
        dispatch(entitiesActions.getEntitiesFailed(e))
    }
}