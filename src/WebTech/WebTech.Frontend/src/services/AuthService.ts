import {localComApi} from "../shared/axios.ts";
import  { AxiosResponse } from 'axios';
import IServerResponsePayload from "../interfaces/models/response/IServerResponsePayload.ts";
import {ENDPOINTS} from "../shared/endoints.ts";
import {ILoginRequest} from "../interfaces/models/request/ILoginRequest.ts";
import IUser from "../interfaces/entities/IUser.ts";
import {IRegisterRequest} from "../interfaces/models/request/IRegisterRequest.ts";


export default class AuthService {

    static async login(userName: string, password: string, fingerprint: string): Promise<AxiosResponse<IServerResponsePayload<string>>> {

        const loginRequest: ILoginRequest = {
            userName,
            password,
            fingerprint
        };

        return localComApi.post<IServerResponsePayload<string>>(ENDPOINTS.USERS.LOGIN, loginRequest);
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

        return localComApi.post<IServerResponsePayload<IUser>>(ENDPOINTS.USERS.REGISTER, registerRequest);
    }

    static async logout()
        : Promise<AxiosResponse<IServerResponsePayload<unknown>>> {

        return localComApi.post<IServerResponsePayload<IUser>>(ENDPOINTS.USERS.LOGOUT);
    }
}