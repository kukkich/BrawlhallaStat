import axios from 'axios';
import AuthService from "../../modules/authentication/services/AuthService";

export const API_URL = 'https://localhost:7231/api'

const $api = axios.create({
    withCredentials: true,
    baseURL: API_URL
})

$api.interceptors.request.use(config => {
    config.headers.Authorization = `Bearer ${localStorage.getItem('token')}`
    console.log('Токен закреплен')
    return config
})

$api.interceptors.response.use(c => c, async error => {
    const originRequest = error.comfig;
    console.log("Отправка")
    if(error.response.status === 401 && !error.config._isRetry) {
        console.log(`Статус ${error.response.status}`)
        console.log(error.config)
        originRequest._isRetry = true
        try {
            const response = await AuthService.refresh();
            localStorage.setItem('token', response.data.accessToken)
            return $api.request(originRequest);
        } catch (e) {
            console.log("Refresh token expired")
        }
    }
    throw error
})

export default $api;