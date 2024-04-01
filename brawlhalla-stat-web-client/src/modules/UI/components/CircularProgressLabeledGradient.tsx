import {CircularProgressProps, Typography} from "@mui/material";
import Box from "@mui/material/Box";
import {GradientCircularProgress} from "./GradientCircularProgress";

export function CircularProgressLabeledGradient(
    props: CircularProgressProps & { value: number },
) {
    return (
        <Box sx={{position: 'relative', display: 'inline-flex'}}>
            <GradientCircularProgress variant="determinate" {...props} />
            <Box
                sx={{
                    top: 0,
                    left: 0,
                    bottom: 0,
                    right: 0,
                    position: 'absolute',
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                }}
            >
                <Typography
                    variant="caption"
                    // component="div"
                    // color="text.secondary"
                >{props.value.toPrecision(3) + "%"}</Typography>
            </Box>
        </Box>
    );
}