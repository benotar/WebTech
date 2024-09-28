import {ILoginRequest} from "../models/request/ILoginRequest.ts";
import IUser from "../entities/IUser.ts";
import {IRegisterRequest} from "../models/request/IRegisterRequest.ts";

export interface IAuthStore {
    isAuthenticated: boolean;
    isLoading: boolean;
    errorCode : string | null;
    token: string | null;
    user : IUser | null;

    login: (params: ILoginRequest) => Promise<string | null>;
    register:(params: IRegisterRequest) => Promise<void>;
    logout: () => Promise<void>;
    refresh: () => Promise<void>;
}