import {ILoginRequest} from "../models/request/ILoginRequest.ts";

export interface IAuthState {
    isAuthenticated: boolean;
    isLoading: boolean;
    errorCode : string | null;
    token: string | null;

    login: (params: ILoginRequest) => Promise<string | null>;
    logout: () => Promise<void>;
    refresh: () => Promise<string>;
}