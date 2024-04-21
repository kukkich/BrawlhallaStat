import {useState} from "react";
import {OverridableStringUnion} from "@mui/types";
import {ButtonPropsColorOverrides} from "@mui/material/Button/Button";

interface ActionFeedbackTimeout {
    succeed: number,
    failed: number
}

export const useActionFeedbackColor = (defaultColor: Color, timeout?: ActionFeedbackTimeout)
    : [Color, () => void, () => void] => {
    if (timeout === undefined) {
        timeout = {succeed: 800, failed: 800}
    }
    const [color, setColor] = useState<string>(defaultColor);

    const onSucceed = () => {
        setColor('success')
        setTimeout(() => {
            setColor(defaultColor);
        }, timeout!.succeed);
    }
    const onFailed = () => {
        setColor('error')
        setTimeout(() => {
            setColor(defaultColor);
        }, timeout!.failed);
    }

    return [color as Color, onSucceed, onFailed];
}

export type Color = OverridableStringUnion<
    'inherit' | 'primary' | 'secondary' | 'success' | 'error' | 'info' | 'warning',
    ButtonPropsColorOverrides
>