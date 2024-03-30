import {useState, FC} from 'react';
import {Button, CircularProgress, Container, TextField, Typography} from '@mui/material';
import {useRootDispatch, useRootSelector} from "../../../store";
import {LoginStatus} from "../store/state";
import {registerAction} from "../store/actions";

interface RegistrationFormProps {
    onSubmit: () => void;
}

const RegistrationForm: FC<RegistrationFormProps> = ({onSubmit}) => {
    const dispatch = useRootDispatch();
    const userState = useRootSelector(state => state.userReducer);

    const [login, setLogin] = useState('');
    const [nickName, setNickName] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [loginError, setLoginError] = useState<string | null>(null);
    const [passwordError, setPasswordError] = useState<string | null>(null);
    const [emailError, setEmailError] = useState<string | null>(null);
    const [buttonColor] = useState<'primary' | 'success' | 'error'>('primary');

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
            return
        }

        onSubmit();
        dispatch(registerAction({login, nickName, password, email}));
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
            <TextField label="NickName" fullWidth margin="normal"
                       value={nickName}
                       onChange={(e) => {
                           setNickName(e.target.value);
                       }}
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
            {userState.errors.map(error =>
                <Typography key={error} variant="body2" color="error">
                    {error}
                </Typography>)
            }
            <Button
                variant="contained"
                //@ts-ignore
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
