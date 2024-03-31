import {FC, FormEvent} from 'react';
import {Button, CircularProgress, Container} from "@mui/material";
import {useRootDispatch, useRootSelector} from "../../../store";
import {getEntitiesAction} from "../store/actions";

export const GetEntitiesButton: FC = () => {
    const dispatch = useRootDispatch();
    const entitiesState = useRootSelector(state => state.entitiesReducer);

    const getEntities = async (event: FormEvent) => {
        event.preventDefault();

        await dispatch(getEntitiesAction())
    }


    return (
        <Container>
            <Button
                variant="contained"
                onClick={getEntities}
                disabled={entitiesState.isFetching}
                endIcon={entitiesState.isFetching
                    ? <CircularProgress size={20} color="inherit"/>
                    : null}
            >
                Get entities
            </Button>
        </Container>
    );
};