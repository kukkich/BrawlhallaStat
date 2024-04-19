import React from 'react';
import {IconButton, Menu, MenuItem, Typography} from "@mui/material";
import {AccountCircle} from "@mui/icons-material";
import {useRootDispatch, useRootSelector} from "../../../../store";
import {logoutAction} from "../../../authentication/store/actions";
import {useNavigate} from "react-router-dom";

const AvatarMenu: React.FC = () => {
    const dispatch = useRootDispatch();
    const login = useRootSelector(state => state.userReducer.user?.login);
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const navigate = useNavigate();

    const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };
    const handleLogout = async () => {
        await dispatch(logoutAction())
        handleClose()
    };
    const handleMoveToProfile = () => {
        navigate('/profile')
        handleClose()
    }

    return (
        <div>
            <IconButton
                size="large"
                onClick={handleMenu}
                color="inherit"
            >
                <Typography variant="h6" style={{ marginRight: 8 }}>
                    {login}
                </Typography>
                <AccountCircle/>
            </IconButton>
            <Menu
                id="menu-appbar"
                anchorEl={anchorEl}
                anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                keepMounted
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                <MenuItem onClick={handleLogout}>Logout</MenuItem>
                <MenuItem onClick={handleMoveToProfile}>Profile</MenuItem>
            </Menu>
        </div>
    );
};

export default AvatarMenu;