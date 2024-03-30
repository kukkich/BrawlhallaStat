import {Backdrop, Fade, Modal} from '@mui/material';
import React, {FC} from 'react';
import {StatisticFilterCreate} from "../../types";
import {FilterForm} from "./FilterForm";
import Box from "@mui/material/Box";

type Props = {
    open: boolean,
    onSubmit: (filter: StatisticFilterCreate) => void,
    onClose: () => void,
};

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
};

export const ModalFilterForm: FC<Props> = ({open, onSubmit, onClose}: Props) => {
    return (
        <Modal
            aria-labelledby="transition-modal-title"
            aria-describedby="transition-modal-description"
            open={open}
            onClose={onClose}
            closeAfterTransition
            slots={{backdrop: Backdrop}}
            slotProps={{
                backdrop: {
                    timeout: 500,
                },
            }}
        >
            <Fade in={open}>
                <Box sx={style}>
                    <FilterForm onSubmit={onSubmit}></FilterForm>
                </Box>
            </Fade>
        </Modal>
    );
};