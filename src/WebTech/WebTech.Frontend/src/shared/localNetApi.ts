import axios, {AxiosInstance} from "axios";
import {LOCAL_NET_API_URL} from "./endoints.ts";
import {useAuthStore} from "../stores/useAuthStore.ts";

export const localNetApi: AxiosInstance = axios.create({
    baseURL: LOCAL_NET_API_URL,
    withCredentials: true,
    headers: {
        'Content-Type': 'application/json'}
});

localNetApi.interceptors.request.use(
    (config) => {
        const {token} = useAuthStore.getState();

        if(token) {
            config.headers.Authorization = `Bearer ${token}`;
        }

        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
)


localNetApi.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error.config;

        if(error.response) {
            if(error.response.status === 401 && !originalRequest._retry){
                originalRequest._retry = true;

                try{
                    const refresh = useAuthStore.getState().refresh;

                    await refresh();

                    const token = useAuthStore.getState().token;

                    originalRequest.headers.Authorization = `Bearer ${token}`;

                    return localNetApi(originalRequest);
                }catch(refreshError) {
                    console.error('Error refreshing token:', refreshError);

                    const logout = useAuthStore.getState().logout;

                    await logout();
                }
            }
        }else  {
            console.error('The request did not reach the server or the server did not respond');
        }

        return Promise.reject(error);
    }
);

export default localNetApi;