import {FC} from 'react';
import {StatisticFilter} from "../../types";
import {useBrawlhallaEntities} from "../../../brawlhallaEntities/hooks/useBrawlhallaEntities";
import {CircularProgress} from "@mui/material";
import {LegendIcon} from "../../../brawlhallaEntities/components/icons/LegendIcon";

type Props = {
    filter: StatisticFilter
};

export const FilterView: FC<Props> = ({filter}: Props) => {
    const [weapons, legends, fetched] = useBrawlhallaEntities(true);

    const legend = filter.legendId !== null
        ? legends.find(x => x.id === filter.legendId)
        : null

    return (
        <>
        {fetched
            ? (legend !== null && legend !== undefined
                ? <LegendIcon width='40' name={legend.name} />
                : <CircularProgress size={20} color="warning"/>)
            : <CircularProgress size={20} color="inherit"/>
        }
        </>
    );
};