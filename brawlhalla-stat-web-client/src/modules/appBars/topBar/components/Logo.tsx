import { FC } from 'react';
import {Typography} from "@mui/material";

const Logo: FC = () => {
    return (
        <Typography variant="h6" component="div" sx={{
            display: { xs: 'none', md: 'flex' },
            fontFamily: 'monospace',
            fontWeight: 700,
            letterSpacing: '.3rem',
            color: 'inherit',
            textDecoration: 'none',
        }}>
            BHStats
        </Typography>
    );
};

export default Logo;