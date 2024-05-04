import React, {FC, useEffect, useState} from 'react';
import {
    Grid,
    Typography,
    TextField,
    Button,
    List,
    ListItem,
    useMediaQuery,
    Theme,
    CircularProgress
} from '@mui/material';
import { useRootSelector } from "../../../store";
import {useNickName} from "../hooks/useNickName";
import {useEmail} from "../hooks/useEmail";
import UserService, {UpdateProfileRequest} from "./services/UserService";
import {useActionFeedbackColor} from "../../UI/hooks/useActionFeedbackColor";

export const ProfilePage: FC = () => {
    const user = useRootSelector(x => x.userReducer.user);

    const isLargerMd = useMediaQuery((theme: Theme) => theme.breakpoints.up('md'));
    const topMargin = isLargerMd ? '5%' : '8px';
    const [buttonColor, onSucceed, onFailed]= useActionFeedbackColor('primary')

    const [nickName, setNickName, nickNameError, validateNickName] = useNickName()
    const [email, setEmail, emailError, validateEmail] = useEmail()
    const [isLoading, setIsLoading] = useState<boolean>(false);

    useEffect(() => {
        if (user === null) {
            return
        }
        setNickName(user.nickName)
        setEmail(user.email)
    }, [])

    const handleSave = async () => {
        const isEmailValid = validateEmail();
        const isNickNameValid = validateNickName();

        if (!isEmailValid || !isNickNameValid){
            return;
        }

        setIsLoading(true);
        const request: UpdateProfileRequest = {
            nickName,
            email
        };

        try {
            console.log('обновляю профиль')
            await UserService.updateProfile(request, user!.id);
            onSucceed()
        } catch (error) {
            onFailed()
        } finally {
            setIsLoading(false);
        }
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
                        error={!!emailError}
                        helperText={emailError || ''}
                    />
                </Grid>
            </Grid>
                <Grid item>
                    <Button variant="contained" color={buttonColor} onClick={handleSave} disabled={isLoading}>
                        {isLoading ? <CircularProgress size={24} /> : 'Save'}
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
