import React from 'react';
import { TextField, Button, Container } from '@mui/material';

interface RegistrationFormProps {
    onSubmit: () => void;
}

const RegistrationForm: React.FC<RegistrationFormProps> = ({ onSubmit }) => {
    return (
        <Container>
            <form onSubmit={onSubmit}>
                <TextField label="Username" fullWidth margin="normal" />
                <TextField label="Email" type="email" fullWidth margin="normal" />
                <TextField label="Password" type="password" fullWidth margin="normal" />
                <Button type="submit" variant="contained" color="primary">
                    Register
                </Button>
            </form>
        </Container>
    );
};

export default RegistrationForm;
