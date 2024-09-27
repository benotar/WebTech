import {create} from 'zustand';
import {persist, createJSONStorage} from 'zustand/middleware'
import {IAuthState} from "../interfaces/store/IAuthState.ts";
import {ILoginRequest} from "../interfaces/models/request/ILoginRequest.ts";
import AuthService from "../services/AuthService.ts";

export const useAuthStore = create<IAuthState>()(persist((set) => ({
    isAuthenticated: false,
    isLoading: false,
    errorCode: null,
    token: null,

    login: async (params: ILoginRequest) : Promise<string> => {
        console.log('login');

        set({isLoading: true});

        const response = await AuthService.login(params.userName, params.password, params.fingerprint);

        set({
            isAuthenticated: response.data.isSucceed,
            token: response.data.data,
            errorCode: response.data.errorCode
        });

        set({isLoading: false});

        return response.data.data;
    },

    logout: async (): Promise<void> => {
        console.log('logout');

        set({isLoading: true});

        await AuthService.logout();

        set({isAuthenticated: false});

        set({isLoading: false});
    },

    refresh: async (): Promise<string> => {
        console.log('refresh');

        set({isLoading: true});

        const response = await AuthService.refresh();

        set({
            isAuthenticated: response.data.isSucceed,
            token: response.data.data,
            errorCode: response.data.errorCode
        });

        set({isLoading: false});

        return response.data.data;
    }

}), {
    name: 'web-tech-storage',
    storage: createJSONStorage(() => sessionStorage)
}));
