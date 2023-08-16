import React from 'react';
import {Typography} from "@mui/material";

const Logo: React.FC = () => {
    return (
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            BHStats
        </Typography>
    );
};

export default Logo;