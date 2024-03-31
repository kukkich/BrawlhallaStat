import {FC} from 'react';
import {ListItemButton, ListItemText} from "@mui/material";

export const StatisticSideBarContent: FC = () => {
    return (
        <ListItemButton>
            {/*<ListItemIcon/>*/}
            <ListItemText primary="Sidebar Item"/>
        </ListItemButton>
    );
};