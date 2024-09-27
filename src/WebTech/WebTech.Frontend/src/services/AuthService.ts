import localComApi from "../shared/localComApi.ts";
import {AxiosResponse} from 'axios';
import IServerResponsePayload from "../interfaces/models/response/IServerResponsePayload.ts";
import {ENDPOINTS} from "../shared/endoints.ts";
import {ILoginRequest} from "../interfaces/models/request/ILoginRequest.ts";
import IUser from "../interfaces/entities/IUser.ts";
import {IRegisterRequest} from "../interfaces/models/request/IRegisterRequest.ts";


export default class AuthService {

    static async login(params: ILoginRequest): Promise<AxiosResponse<IServerResponsePayload<string>>> {


        return await localComApi.post<IServerResponsePayload<string>>(ENDPOINTS.USERS.LOGIN, {
            userName: params.userName,
            password: params.password,
            fingerprint: params.fingerprint
        });
    }

    static async register(userName: string, password: string, firstName: string, lastName: string, birthDate: string, address: string)
        : Promise<AxiosResponse<IServerResponsePayload<IUser>>> {

        const registerRequest: IRegisterRequest = {
            userName,
            password,
            firstName,
            lastName,
            birthDate,
            address
        };

        return await localComApi.post<IServerResponsePayload<IUser>>(ENDPOINTS.USERS.REGISTER, registerRequest);
    }

    static async logout()
        : Promise<AxiosResponse<IServerResponsePayload<unknown>>> {

        return await localComApi.post<IServerResponsePayload<IUser>>(ENDPOINTS.USERS.LOGOUT);
    }

    static async refresh(): Promise<AxiosResponse<IServerResponsePayload<string>>> {
        return await localComApi.post<IServerResponsePayload<string>>(ENDPOINTS.REFRESH);
    }
}