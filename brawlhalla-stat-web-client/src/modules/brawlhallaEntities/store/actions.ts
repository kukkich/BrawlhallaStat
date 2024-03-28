import {AppDispatch} from "../../../store";
import {entitiesActions} from "./reducer";
import entitiesService from "../services/entitiesService";
import {getErrorResponse} from "../../../api/tools/getErrorResponse";

export const getEntitiesAction = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(entitiesActions.getEntitiesStart())
        const response = await entitiesService.getEntities();
        dispatch(entitiesActions.getEntitiesSuccess(response));
    } catch (e: any){
        dispatch(entitiesActions.getEntitiesFailed(getErrorResponse(e)))
    }
}