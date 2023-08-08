import React from 'react';
import { TextField, Button, Container } from '@mui/material';

interface LoginFormProps {
    onSubmit: () => void; // Передайте функцию для обработки отправки формы
}

const LoginForm: React.FC<LoginFormProps> = ({ onSubmit }) => {
    return (
        <Container>
            <form onSubmit={onSubmit}>
                <TextField label="Username" fullWidth margin="normal" />
                <TextField label="Password" type="password" fullWidth margin="normal" />
                <Button type="submit" variant="contained" color="primary">
                    Log In
                </Button>
            </form>
        </Container>
    );
};

export default LoginForm;
