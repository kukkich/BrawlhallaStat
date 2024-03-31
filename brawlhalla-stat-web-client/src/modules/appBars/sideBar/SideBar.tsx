import {Grid, List, Paper} from "@mui/material";
import {FC, ReactNode} from "react";

type SideBarProps = {
    content: ReactNode;
}

const SideBar: FC<SideBarProps> = ({ content }: SideBarProps) => {
    return (
        <Grid item xs>
            <Paper elevation={2} sx={{height: '100vh'}}>
                <List>
                    {content}
                </List>
            </Paper>
        </Grid>
    );
};

export default SideBar;
