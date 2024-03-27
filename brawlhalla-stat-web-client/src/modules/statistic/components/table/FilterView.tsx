import {FC, useEffect} from 'react';
import {StatisticFilter} from "../../types";
import {useRootDispatch, useRootSelector} from "../../../../store";
import {getEntitiesAction} from "../../../brawlhallaEntities/store/actions";

type Props = {
    filter: StatisticFilter
};

export const FilterView: FC<Props> = ({filter : StatisticFilter}: Props) => {
    const dispatch = useRootDispatch();

    const entities = useRootSelector(state => state.entitiesReducer);
    // const loading = !entities.fetched || entities.isFetching;

    // useEffect(() => {
    //     (async () => {
    //         if (!entities.fetched && !entities.isFetching) {
    //             await dispatch(getEntitiesAction());
    //         }
    //     })();
    // })

    // const legend = entities.legends.find(x => x.id === filter.legendId)

    return (
        <>
        </>
    );
};