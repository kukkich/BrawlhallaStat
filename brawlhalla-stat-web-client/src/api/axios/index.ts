import axios from 'axios';
import AuthService from "../../modules/authentication/services/AuthService";

export const API_URL = 'https://localhost:7231/api'

const $api = axios.create({
    withCredentials: true,
    baseURL: API_URL
})

$api.interceptors.request.use(config => {
    config.headers.Authorization = `Bearer ${localStorage.getItem('token')}`
    return config
})

$api.interceptors.response.use(c => c, async error => {
    const originalRequest = error.config;
    console.log("Отправка");
    console.log(error);
    if (error.code === 'ERR_NETWORK') {
        throw error
    }
    if (error.response.status === 401 && originalRequest._isRetry === undefined) {
        console.log(originalRequest._isRetry);
        originalRequest._isRetry = true;
        console.log(originalRequest);
        try {
            const response = await AuthService.refresh();
            localStorage.setItem('token', response.data.accessToken);
            originalRequest.headers['Authorization'] = `Bearer ${response.data.accessToken}`;
            return await $api.request(originalRequest)
        } catch (e) {
            console.log("Refresh token expired");
        }
    }
    throw error;
})

export default $api;