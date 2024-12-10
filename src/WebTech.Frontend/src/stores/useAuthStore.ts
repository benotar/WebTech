import {create} from 'zustand';
import {persist, createJSONStorage} from 'zustand/middleware'
import {ILoginRequest} from "../interfaces/models/request/ILoginRequest.ts";
import AuthService from "../services/AuthService.ts";
import UserService from "../services/UserService.ts";
import {IAuthStore} from "../interfaces/stores/IAuthStore.ts";
import {IRegisterRequest} from "../interfaces/models/request/IRegisterRequest.ts";


const handleError = (error: unknown): string => {
    console.log('Error:', error);
    return (error as any).response?.data?.message || 'An unexpected error occurred. Please try again later.';
};

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

            if (!response.data.isSucceed) {
                const errorCode = response.data.errorCode;

                throw new Error(typeof errorCode === 'string' ? errorCode : 'Unknown error occurred');
            }

            set({
                isAuthenticated: response.data.isSucceed,
                token: response.data.data
            });

            try {
                const getUserResponse = await UserService.GetMe();

                set({user: getUserResponse.data.data});
            } catch (error) {
                const errorMessage = handleError(error);

                set({
                    isAuthenticated: false,
                    errorCode: errorMessage
                });

                throw new Error(errorMessage); // Генеруємо виключення з повідомленням про помилку
            }

            return response.data.data;
        } catch (error) {

            const errorMessage = handleError(error);

            set({
                isAuthenticated: false,
                errorCode: errorMessage
            });

            throw new Error(errorMessage);

        } finally {
            set({isLoading: false});
        }
    },
    register: async (params: IRegisterRequest): Promise<void> => {
        console.log('register');

        set({isLoading: true});

        try {
            const response = await AuthService.register(params);

            if (!response.data.isSucceed) {
                const errorCode = response.data.errorCode;

                throw new Error(typeof errorCode === 'string' ? errorCode : 'Unknown error occurred');
            }

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

        try {
            await AuthService.logout();

            set({
                isAuthenticated: false,
                errorCode: null,
                token: null,
                user: null
            });
        } catch (error) {
            console.log('Logout error:', error);
        } finally {
            set({isLoading: false});
        }
    },

    refresh: async (): Promise<void> => {
        console.log('refresh');

        set({isLoading: true});

        try {
            const response = await AuthService.refresh();

            set({
                isAuthenticated: response.data.isSucceed,
                token: response.data.data,
                errorCode: response.data.errorCode
            });
        } catch (error) {
            console.log('Refresh error: ', error);
        } finally {
            set({isLoading: false});
        }
    }

}), {
    name: 'web-tech-storage',
    storage: createJSONStorage(() => sessionStorage)
}));
