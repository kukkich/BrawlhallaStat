import {CircularProgress, CircularProgressProps, useTheme} from "@mui/material";

export function GradientCircularProgress(
    props: CircularProgressProps & { value: number },
) {
    const theme = useTheme()
    console.log(theme)
    return (
        <>
            <svg width={0} height={0}>
                <defs>
                    <linearGradient id="my_gradient" x1="0%" y1="0%" x2="0%" y2="100%">
                        {/*<stop offset="40%" stopColor={theme.palette.success.main} />*/}
                        <stop offset="0%" stopColor={theme.palette.secondary.main} />
                        <stop offset="100%" stopColor={theme.palette.primary.main} />
                        {/*<stop offset="0%" stopColor="#e01cd5" />*/}
                        {/*<stop offset="100%" stopColor="#1CB5E0" />*/}
                    </linearGradient>
                </defs>
            </svg>
            <CircularProgress {...props} sx={{ 'svg circle': { stroke: 'url(#my_gradient)' } }} />
        </>
    );
}