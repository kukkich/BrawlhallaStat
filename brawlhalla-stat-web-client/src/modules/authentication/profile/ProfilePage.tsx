import React, {FC, useEffect} from 'react';
import { Grid, Typography, TextField, Button, List, ListItem, useMediaQuery, Theme } from '@mui/material';
import { useRootSelector } from "../../../store";
import {useNickName} from "../hooks/useNickName";
import {useEmail} from "../hooks/useEmail";

export const ProfilePage: FC = () => {
    const user = useRootSelector(x => x.userReducer.user);

    const isLargerMd = useMediaQuery((theme: Theme) => theme.breakpoints.up('md'));
    const topMargin = isLargerMd ? '5%' : '8px';

    const [nickName, setNickName, nickNameError, validateNickName] = useNickName()
    const [email, setEmail, emailError, validateEmail] = useEmail()

    useEffect(() => {
        console.log("update")
        if (user === null) {
            return
        }
        setNickName(user.nickName)
        setEmail(user.email)
    }, [])

    const handleSave = () => {
        const isEmailValid = validateEmail();
        const isNickNameValid = validateNickName();
        console.log(isEmailValid);
        console.log(isNickNameValid)
        if (!isEmailValid || !isNickNameValid){
            return;
        }

        //todo handle Save
    }

    if (!user) {
        return <></>
    }

    return (
        <Grid item container justifyContent="center" alignItems="center" sx={{ mt: topMargin }}>
            <Grid item xs={10} md={6} container direction="column" spacing={6}>
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
                        value={nickName}
                        onChange={(e) => setNickName(e.target.value)}
                        error={!!nickNameError}
                        helperText={nickNameError || ''}
                    />
                </Grid>
            </Grid>
            <Grid item container direction="row" alignItems="center" spacing={2}>
                <Grid item xs>
                    <TextField
                        label="Email"
                        variant="outlined"
                        fullWidth
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        error={!!emailError}  // Добавляем проверку ошибки
                        helperText={emailError || ''}  // Показываем текст ошибки если он есть
                    />
                </Grid>
            </Grid>

                <Grid item>
                    <Button variant="contained" color="primary" onClick={handleSave}>
                        Save
                    </Button>
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