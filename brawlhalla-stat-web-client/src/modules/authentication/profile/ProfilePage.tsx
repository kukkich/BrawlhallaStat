import { Grid, List, ListItem, TextField, Theme, Typography, useMediaQuery, Button } from '@mui/material';
import React, { FC } from 'react';
import { useRootSelector } from "../../../store";

export const ProfilePage: FC = () => {
    const user = useRootSelector(x => x.userReducer.user);

    const isLargerMd = useMediaQuery((theme: Theme) => theme.breakpoints.up('md'));
    const topMargin = isLargerMd ? '5%' : '8px';

    if (!user) {
        return <></>
    }

    return (
        <Grid item container
              justifyContent="center" alignItems="center"
              sx={{ mt: topMargin }}
        >
            <Grid item xs={10} md={6}
                  container direction="column"
                  spacing={6}
            >
                <Grid item>
                    <Typography variant="body1" color="text.secondary">ID: {user.id}</Typography>
                    <Typography variant="subtitle1" color="text.primary">Login: {user.login}</Typography>
                </Grid>
                <Grid item container direction="row" alignItems="center" spacing={2}>
                    <Grid item xs>
                        <TextField
                            label="Nick Name"
                            variant="outlined"
                            fullWidth
                            defaultValue={user.nickName}
                            onChange={(e) => console.log('Nick Name change:', e.target.value)}
                        />
                    </Grid>
                    <Grid item>
                        <Button variant="contained" color="primary" onClick={() => {}}>Save</Button>
                    </Grid>
                </Grid>
                <Grid item container direction="row" alignItems="center" spacing={2}>
                    <Grid item xs>
                        <TextField
                            label="Email"
                            variant="outlined"
                            fullWidth
                            defaultValue={user.email}
                            onChange={(e) => console.log('Email Change:', e.target.value)}
                        />
                    </Grid>
                    <Grid item>
                        <Button variant="contained" color="primary" onClick={() => {}}>Save</Button>
                    </Grid>
                </Grid>
                <Grid item container spacing={1}>
                    <Grid item xs={12} sm={6}>
                        <Typography variant="h6" color="text.primary">Roles</Typography>
                        <List>
                            {user.roles.map((role, index) => (
                                <ListItem key={index}>{role.name}</ListItem>
                            ))}
                        </List>
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <Typography variant="h6" color="text.primary">Claims</Typography>
                        <List>
                            {user.claims.map((claim, index) => (
                                <ListItem key={index}>{claim.name} - {claim.value}</ListItem>
                            ))}
                        </List>
                    </Grid>
                </Grid>
            </Grid>
        </Grid>
    );
};
