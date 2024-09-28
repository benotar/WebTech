import axios, {AxiosInstance} from "axios";
import {LOCAL_NET_API_URL} from "./endoints.ts";
import {useAuthStore} from "../stores/useAuthStore.ts";

export const localNetApi: AxiosInstance = axios.create({
    baseURL: LOCAL_NET_API_URL,
    withCredentials: true,
    headers: {
        'Content-Type': 'application/json'}
});

// localNetApi.interceptors.request.use((request) => {
//
//     const {token} = useAuthStore.getState();
//
//     if (token) {
//         request.headers.Authorization = `Bearer ${token}`;
//     }
//
//     console.log('[INTERCEPTOR] localNetApi REQUEST');
//     console.log(request);
//
//     return request;
// });

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
                }catch(error) {
                    // Handle refresh token error or redirect to login
                }
            }
        }else  {
            console.error('The request did not reach the server or the server did not respond');
        }

        return Promise.reject(error);
    }
);


// localNetApi.interceptors.response.use(
//     (response) => {
//       return response;
//     },
//     async (error) => {
//         const originalRequest = error.config;
//
//         if(error.response.status === 401 && !originalRequest._retry) {
//             originalRequest._retry = true;
//
//             const {refresh} = useAuthStore.getState();
//
//             await refresh();
//
//             const{token} = useAuthStore.getState();
//
//             if(token) {
//                 try {
//                     return localNetApi(originalRequest);
//                 }catch(error) {
//                     // Handle token refresh failure
//                     // mostly logout the user and re-authenticate by login again
//                 }
//             }
//         }
//         return Promise.reject(error);
//     }
// );

// localNetApi.interceptors.request.use(
//     (config) => {
//         console.log('[INTERCEPTOR] RESPONSE SUCCESS');
//         console.log(config);
//
//         return config;
//     },
//     async (error) => {
//         console.log('[INTERCEPTOR] localNetApi RESPONSE ERROR');
//         console.error(error);
//
//         const originalRequest = error.config;
//
//         if(error.response.status == 401 && error.config && !error.config._isRetry) {
//
//             originalRequest._isRetry = true;
//
//             try {
//                 const {isAuthenticated, refresh} = useAuthStore.getState();
//
//                 console.log('STATUS 401, refreshing...');
//
//                 await refresh();
//
//                 console.log('REFRESHED, AUTHENTICATED: ' + isAuthenticated)
//
//                 return localNetApi.request(originalRequest);
//             } catch(e) {
//                 console.log(`Authorization failed with error - ${e}`);
//             }
//         }
//
//         throw error;
//     }
// );

export default localNetApi;