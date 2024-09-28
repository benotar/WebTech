import {create} from 'zustand';
import {persist, createJSONStorage} from 'zustand/middleware'
import {ILoginRequest} from "../interfaces/models/request/ILoginRequest.ts";
import AuthService from "../services/AuthService.ts";
import UserService from "../services/UserService.ts";
import {IAuthStore} from "../interfaces/stores/IAuthStore.ts";
import {IRegisterRequest} from "../interfaces/models/request/IRegisterRequest.ts";

export const useAuthStore = create<IAuthStore>()(persist((set) => ({
    isAuthenticated: false,
    isLoading: false,
    errorCode: null,
    token: null,
    user: null,

    login: async (params: ILoginRequest): Promise<string | null> => {

        console.log('login');

        set({isLoading: true});

        try {
            const response = await AuthService.login(params);

            set({
                isAuthenticated: response.data.isSucceed,
                token: response.data.data,
                errorCode: response.data.errorCode
            });

            if (response.data.isSucceed) {
                const getUserResponse = await UserService.GetMe();

                set({user: getUserResponse.data.data});
            }


            return response.data.data;
        } catch (error) {
            console.error('Login error:', error);

            set({
                isAuthenticated: false,
                errorCode: 'Login failed'
            });

            return null;
        } finally {
            set({isLoading: false});

        }
    },
    register: async (params: IRegisterRequest): Promise<void> => {
        console.log('register');

        set({isLoading: true});

        try {

            await AuthService.register(params);

        } catch (error) {
            console.error('Registration error: ', error);
            set({errorCode: 'An error occurred during registration.'});

            throw new Error('Registration failed');
        } finally {
            set({isLoading: false});
        }
    },
    logout: async (): Promise<void> => {
        console.log('logout');

        set({isLoading: true});

        await AuthService.logout();

        set({
            isAuthenticated: false,
            errorCode: null,
            token: null,
            user: null
        });

        set({isLoading: false});
    },

    refresh: async (): Promise<void> => {
        console.log('refresh');

        set({isLoading: true});

        const response = await AuthService.refresh();

        set({
            isAuthenticated: response.data.isSucceed,
            token: response.data.data,
            errorCode: response.data.errorCode
        });

        set({isLoading: false});
    }

}), {
    name: 'web-tech-storage',
    storage: createJSONStorage(() => sessionStorage)
}));
