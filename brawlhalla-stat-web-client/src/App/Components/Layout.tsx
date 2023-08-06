import React from 'react';
import { Grid, List, ListItem, ListItemIcon, ListItemText} from '@mui/material';
import TopBar from "./TopBar";

interface LayoutProps {
    children: React.ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
    return (
        <Grid container>
            {/* AppBar (Top Panel) */}
            <TopBar/>

            {/* Sidebar */}
            <Grid item xs={2}>
                <List>
                    {/* Add your sidebar items here */}
                    <ListItem button>
                        <ListItemIcon>
                            {/* Add icon */}
                        </ListItemIcon>
                        <ListItemText primary="Sidebar Item" />
                    </ListItem>
                </List>
            </Grid>

            {/* Main Content */}
            <Grid item xs={10}>
                <div style={{ padding: '16px' }}>
                    {/* Main content area */}
                    <Grid container spacing={4}>
                        {/* Content with 12 columns */}
                        {children}
                    </Grid>
                </div>
            </Grid>
        </Grid>
    );
};

export default Layout;