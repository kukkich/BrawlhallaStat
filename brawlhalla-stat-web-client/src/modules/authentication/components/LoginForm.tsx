import React, {useState} from 'react';
import {Button, Container, TextField} from '@mui/material';

interface LoginFormProps {
    onSubmit: () => void;
}

const LoginForm: React.FC<LoginFormProps> = ({onSubmit}) => {

    const [username, setUsername] = useState(''); // Значение из поля Username
    const [password, setPassword] = useState(''); // Значение из поля Password

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();

        //...

        onSubmit();
    };

    return (
        <Container>
            <TextField label="Username" fullWidth margin="normal"
                       value={username}
                       onChange={(e) => setUsername(e.target.value)}
            />
            <TextField label="Password" fullWidth margin="normal" type="password"
                       value={password}
                       onChange={(e) => setPassword(e.target.value)}
            />
            <Button variant="contained" color="primary"
                    onClick={handleSubmit}
            >
                Log In
            </Button>
        </Container>
    );
};

export default LoginForm;
