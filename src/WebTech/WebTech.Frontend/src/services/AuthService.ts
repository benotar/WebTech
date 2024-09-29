import localComApi from "../shared/localComApi.ts";
import {AxiosResponse} from 'axios';
import IServerResponsePayload from "../interfaces/models/response/IServerResponsePayload.ts";
import {ENDPOINTS} from "../shared/endoints.ts";
import {ILoginRequest} from "../interfaces/models/request/ILoginRequest.ts";
import IUser from "../interfaces/entities/IUser.ts";
import {IRegisterRequest} from "../interfaces/models/request/IRegisterRequest.ts";

export default class AuthService {

    static async login(params: ILoginRequest): Promise<AxiosResponse<IServerResponsePayload<string>>> {
        return await localComApi.post<IServerResponsePayload<string>>(ENDPOINTS.USERS.LOGIN, params);
    }

    static async register(params: IRegisterRequest)
        : Promise<AxiosResponse<IServerResponsePayload<IUser>>> {
        return await localComApi.post<IServerResponsePayload<IUser>>(ENDPOINTS.USERS.REGISTER, params);
    }

    static async logout()
        : Promise<AxiosResponse<IServerResponsePayload<unknown>>> {
        return await localComApi.post<IServerResponsePayload<IUser>>(ENDPOINTS.USERS.LOGOUT);
    }

    static async refresh(): Promise<AxiosResponse<IServerResponsePayload<string>>> {
        return await localComApi.post<IServerResponsePayload<string>>(ENDPOINTS.REFRESH);
    }
}