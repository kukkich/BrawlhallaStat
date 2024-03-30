import {useState} from "react";

interface ActionFeedbackTimeout {
    succeed: number,
    failed: number
}

export const useActionFeedbackColor = (defaultColor: string, timeout?: ActionFeedbackTimeout)
    : [string, () => void, () => void] => {
    if (timeout === undefined) {
        timeout = {succeed: 500, failed: 500}
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

    return [color, onSucceed, onFailed];
}