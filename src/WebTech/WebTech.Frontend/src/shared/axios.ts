import AxiosInstance = Axios.AxiosInstance;
import {LOCAL_COM_API_URL, LOCAL_NET_API_URL}    from "./endoints.ts";

const localComApi : AxiosInstance = axios.create({
    baseURL: LOCAL_COM_API_URL,
    withCredentials: false
});

const localNetApi : AxiosInstance = axios.create({
    baseURL: LOCAL_NET_API_URL,
    withCredentials: true
});