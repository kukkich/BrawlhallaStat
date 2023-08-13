import React, {useEffect, useState} from 'react';
import {Button, CircularProgress, Container, TextField} from '@mui/material';
import {useRootDispatch, useRootSelector} from "../../../store";
import {LoginStatus} from "../store/State";
import {registerAction} from "../store/actions";

interface RegistrationFormProps {
    onSubmit: () => void;
}

const RegistrationForm: React.FC<RegistrationFormProps> = ({onSubmit}) => {
    const dispatch = useRootDispatch();
    const userState = useRootSelector(state => state.userReducer);

    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [loginError, setLoginError] = useState<string | null>(null);
    const [passwordError, setPasswordError] = useState<string | null>(null);
    const [emailError, setEmailError] = useState<string | null>(null);

    const [buttonColor, setButtonColor] = useState<'primary' | 'success' | 'error'>('primary');
    useEffect(() => {
        if (buttonColor === 'success') {
            const timeout = setTimeout(() => {
                setButtonColor('primary');
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

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        let anyErrors: boolean = false;

        if (!/^[a-zA-Z0-9_.-]{4,}$/.test(login)) {
            setLoginError('Login must be at least 4 characters long and can only contain English letters, digits, and _-.');
            anyErrors = true
        }
        if (!/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(password)) {
            setPasswordError('Password must be at least 8 characters long and contain both letters and numbers.');
            anyErrors = true
        }
        if (!/^[\w.-]+@[a-zA-Z\d.-]+\.[a-zA-Z]{2,}$/.test(email)) {
            setEmailError('Invalid email');
            anyErrors = true;
        }

        if (anyErrors) {
            setButtonColor('error');
            return
        }

        onSubmit();
        dispatch(registerAction({login, password, email}));
    };

    return (
        <Container>
            <TextField label="Login" fullWidth margin="normal"
                       value={login}
                       onChange={(e) => {
                           setLogin(e.target.value);
                           setLoginError(null);
                       }}
                       error={Boolean(loginError)}
                       helperText={loginError}
            />
            <TextField label="Email" fullWidth margin="normal"
                       value={email}
                       onChange={(e) => {
                           setEmail(e.target.value);
                           setEmailError(null);
                       }}
                       error={Boolean(emailError)}
                       helperText={emailError}
            />
            <TextField label="Password" fullWidth margin="normal" type="password"
                       value={password}
                       onChange={(e) => {
                           setPassword(e.target.value);
                           setPasswordError(null);
                       }}
                       error={Boolean(passwordError)}
                       helperText={passwordError}
            />
            <Button
                variant="contained"
                color={buttonColor}
                onClick={handleSubmit}
                disabled={userState.status === LoginStatus.loginning}
                endIcon={userState.status === LoginStatus.loginning
                    ? <CircularProgress size={20} color="inherit"/>
                    : null}
            >
                Register
            </Button>
        </Container>
    );
};

export default RegistrationForm;
