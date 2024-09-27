import axios, {AxiosInstance} from "axios";
import {LOCAL_COM_API_URL} from "./endoints.ts";
import {useAuthStore} from "../store/store.ts";

export const localComApi: AxiosInstance = axios.create({
    baseURL: LOCAL_COM_API_URL,
    withCredentials: false
});

localComApi.interceptors.request.use((request) => {

    const {token} = useAuthStore.getState();

    if (token) {
        request.headers.Authorization = `Bearer ${token}`;
    }

    console.log('[INTERCEPTOR] localComApi REQUEST');
    console.log(request);

    return request;
})

localComApi.interceptors.request.use(response => {
    return response;
}, async error => {

    console.error('[INTERCEPTOR] localComApi RESPONSE ERROR');
    console.error(error);


    const originalRequest = error.config;

    if (error.response.status == 401 && originalRequest && !originalRequest._isRetry) {

        try {

            const {isAuthenticated, refresh} = useAuthStore.getState();

            console.log('Status 401, refreshing...');

            await refresh();

            console.log('REFRESHED, AUTHENTICATED: ' + isAuthenticated);

            return localComApi.request(originalRequest);
        } catch (e) {
            console.log('Not authorized');
        }

    }
    throw error;
})