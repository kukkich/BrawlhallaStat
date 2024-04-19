import {useState} from "react";

export const useEmail = () :
    [
        string,
        (value: (((prevState: string) => string) | string)) => void,
            string | null,
        () => boolean
    ] => {
    const [email, setEmail] = useState('');
    const [error, setError] = useState<string | null>(null);

    const validate = () => {
        if (!/^[\w.-]+@\w+\.\w{2,7}$/.test(email)) {
            setError('Invalid email');
            return false
        }
        return true;
    }

    return [email, setEmail, error, validate]
}