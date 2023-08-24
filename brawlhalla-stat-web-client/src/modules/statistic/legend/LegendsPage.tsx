import {FC} from 'react';
import {Box, Grid, styled} from "@mui/material";

const Item = styled('div')(({theme}) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    border: '1px solid',
    borderColor: theme.palette.mode === 'dark' ? '#444d58' : '#ced7e0',
    padding: theme.spacing(1),
    borderRadius: '4px',
    textAlign: 'center',
}));

const LegendsPage: FC = () => {
    return (
        <>
            <Grid item>
                <Box sx={{flexGrow: 1}}>
                    <Grid container columnSpacing={{xs: 1, sm: 2, md: 3}}>
                        <Grid item xs={2}>
                            <Item>xs=8</Item>
                        </Grid>
                        <Grid item xs={4}>
                            <Item>xs=4</Item>
                        </Grid>
                        <Grid item xs={6}>
                            <Item>xs=4</Item>
                        </Grid>
                        <Grid item xs={8}>
                            <Item>xs=8</Item>
                        </Grid>
                    </Grid>
                </Box>
            </Grid>
        </>
    );
};

export default LegendsPage;