import {AppDispatch} from "../../../store";
import {entitiesActions} from "../../brawlhallaEntities/store/reducer";
import entitiesService from "../../brawlhallaEntities/services/entitiesService";

export const submitForm = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(entitiesActions.getEntitiesStart())
        const response = await entitiesService.getEntities();
        dispatch(entitiesActions.getEntitiesSuccess(response));
    } catch (e: any){
        dispatch(entitiesActions.getEntitiesFailed(e))
    }
}

export const fetchStatistics = () => async (dispatch: AppDispatch) => {
    try{

    } catch (e: any){

    }
}