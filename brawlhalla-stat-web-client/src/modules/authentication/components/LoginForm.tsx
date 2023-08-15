import React, {useEffect, useState} from 'react';
import {Button, CircularProgress, Container, TextField, Typography} from '@mui/material';
import {useRootDispatch, useRootSelector} from '../../../store';
import {loginAction} from '../store/actions';
import {LoginStatus} from '../store/State';
import {useNavigate} from "react-router-dom";

interface LoginFormProps {
    onSubmit: () => void;
}

const LoginForm: React.FC<LoginFormProps> = ({onSubmit}) => {
    const dispatch = useRootDispatch();
    const userState = useRootSelector(state => state.userReducer);

    const navigate = useNavigate();

    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const [loginError, setLoginError] = useState<string | null>(null);
    const [passwordError, setPasswordError] = useState<string | null>(null);
    const [buttonColor, setButtonColor] = useState<'primary' | 'success' | 'error'>('primary');

    useEffect(() => {
        if (buttonColor === 'success') {
            const timeout = setTimeout(() => {
                setButtonColor('primary');
                navigate("/")
            }, 650);

            return () => clearTimeout(timeout);
        }

        if (buttonColor === 'error') {
            const timeout = setTimeout(() => {
                setButtonColor('primary');
            }, 2000);

            return () => clearTimeout(timeout);
        }
    }, [buttonColor]);
    useEffect(() => {
        if (userState.status === LoginStatus.authorized) {
            setButtonColor('success');
        }
    }, [userState.status]);
    useEffect(() => {
        if (userState.errors.length > 0) {
            setButtonColor('error');
        }
    }, [userState.errors]);
    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        if (userState.status === LoginStatus.authorized) {
            return
        }
        let anyErrors: boolean = false;

        if (!/^[a-zA-Z0-9_.-]{4,}$/.test(login)) {
            setLoginError('Login must be at least 4 characters long and can only contain English letters, digits, and _-.');
            anyErrors = true
        }
        // if (!/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(password)) {
        //     setPasswordError('Password must be at least 8 characters long and contain both letters and numbers.');
        //     anyErrors = true
        // }

        if (anyErrors) {
            setButtonColor('error');
            return
        }

        onSubmit();
        await dispatch(loginAction({login, password}));
    };

    return (
        <Container>
            <TextField label="Login"
                       fullWidth margin="normal"
                       value={login}
                       onChange={(e) => {
                           setLogin(e.target.value);
                           setLoginError(null);
                       }}
                       error={Boolean(loginError)}
                       helperText={loginError}
            />
            <TextField
                label="Password" type="password"
                fullWidth margin="normal"
                value={password}
                onChange={(e) => {
                    setPassword(e.target.value);
                    setPasswordError(null);
                }}
                error={Boolean(passwordError)}
                helperText={passwordError}
            />
            {userState.errors.map(error =>
                <Typography key={error} variant="body2" color="error">
                    {error}
                </Typography>)
            }

            <Button
                variant="contained"
                color={buttonColor}
                onClick={handleSubmit}
                disabled={userState.status === LoginStatus.loginning}
                endIcon={userState.status === LoginStatus.loginning
                    ? <CircularProgress size={20} color="inherit"/>
                    : null}
            >
                Log In
            </Button>
        </Container>
    );
};

export default LoginForm;
