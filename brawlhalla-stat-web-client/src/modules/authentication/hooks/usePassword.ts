import {useState} from "react";

export const usePassword = () :
    [
        string,
        (value: (((prevState: string) => string) | string)) => void,
        string | null,
        () => boolean
    ] => {
    const [password, setPassword] = useState('');
    const [error, setError] = useState<string | null>(null);

    const validate = () => {
        if (!/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(password)) {
            setError('Password must be at least 8 characters long and contain both letters and numbers.');
            return false
        }
        return true;
    }

    return [password, setPassword, error, validate]
}