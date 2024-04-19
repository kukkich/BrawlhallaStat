import {useState} from "react";

export const useNickName = () :
    [
        string,
        (value: (((prevState: string) => string) | string)) => void,
        string | null,
        () => boolean
    ] => {
    const [nickName, setNickName] = useState('');
    const [error, setError] = useState<string | null>(null);

    const validate = () => {
        if (nickName === '') {
            setError('Nick name cannot be empty')
            return false
        }
        return true;
    }

    return [nickName, setNickName, error, validate]
}