import React, { useState } from 'react';
import { Container, Paper, Tab, Tabs } from '@mui/material';
import LoginForm from './LoginForm';
import RegistrationForm from './RegistrationForm';
import {lightTheme} from "../../theme";
import {ThemeProvider} from "@mui/material/styles";

const AuthTabs: React.FC = () => {
    const [selectedTab, setSelectedTab] = useState(0);

    const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
        setSelectedTab(newValue);
    };

    const handleSubmit = () => {
        // Обработка отправки формы (вы можете добавить свою логику здесь)
    };

    return (
        <Container component="main" maxWidth="xs">
            <Paper elevation={3} style={{ padding: '20px' }}>
                <Tabs
                    value={selectedTab}
                    onChange={handleTabChange}
                    centered
                    textColor="secondary"
                    indicatorColor="secondary"
                >
                    <Tab color="secondary" label="Login" />
                    <Tab label="Register" />
                </Tabs>
                {selectedTab === 0 && <LoginForm onSubmit={handleSubmit} />}
                {selectedTab === 1 && <RegistrationForm onSubmit={handleSubmit} />}
            </Paper>
        </Container>
    );
};

export default AuthTabs;
