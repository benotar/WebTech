import axios, {AxiosInstance} from "axios";
import {LOCAL_COM_API_URL, LOCAL_NET_API_URL}    from "./endoints.ts";

export const localComApi : AxiosInstance = axios.create({
    baseURL: LOCAL_COM_API_URL,
    withCredentials: false
});

export const localNetApi : AxiosInstance = axios.create({
    baseURL: LOCAL_NET_API_URL,
    withCredentials: true
});

localNetApi.interceptors.request.use((request) => {
    request.headers.Authorization = `Bearer ${localStorage.getItem('token')}`;

    console.log('[INTERCEPTOR] REQUEST');
    console.log(request);

    return request;
});
