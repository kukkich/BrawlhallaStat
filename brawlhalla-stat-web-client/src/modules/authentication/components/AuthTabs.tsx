import React, {useState} from 'react';
import {Container, Paper, Tab, Tabs} from '@mui/material';
import LoginForm from './LoginForm';
import RegistrationForm from './RegistrationForm';

const AuthTabs: React.FC = () => {
    const [selectedTab, setSelectedTab] = useState(0);

    const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
        setSelectedTab(newValue);
    };

    const handleSubmit = () => {
        // Обработка отправки формы (вы можете добавить свою логику здесь)
    };

    return (
        <Container maxWidth="xs" sx={{color: 'primary'}}>
            <Paper elevation={6} style={{padding: '20px'}}>
                <Tabs
                    value={selectedTab}
                    onChange={handleTabChange}
                    centered
                    textColor="secondary"
                    indicatorColor="secondary"
                >
                    <Tab color="secondary" label="Login"/>
                    <Tab label="Register"/>
                </Tabs>
                {selectedTab === 0 && <LoginForm onSubmit={handleSubmit}/>}
                {selectedTab === 1 && <RegistrationForm onSubmit={handleSubmit}/>}
            </Paper>
        </Container>
    );
};

export default AuthTabs;
