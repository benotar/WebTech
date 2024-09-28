import {ILoginRequest} from "../models/request/ILoginRequest.ts";
import IUser from "../entities/IUser.ts";

export interface IAuthStore {
    isAuthenticated: boolean;
    isLoading: boolean;
    errorCode : string | null;
    token: string | null;
    user : IUser | null;

    login: (params: ILoginRequest) => Promise<string | null>;
    logout: () => Promise<void>;
    refresh: () => Promise<void>;
}