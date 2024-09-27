import  { AxiosResponse } from 'axios';
import IUser from "../interfaces/entities/IUser.ts";
import {localComApi} from "../shared/axios.ts";
import {ENDPOINTS} from "../shared/endoints.ts";
import IServerResponsePayload from "../interfaces/models/response/IServerResponsePayload.ts";




export default class UserService {



    static async GetMe(token:string | null) : Promise<AxiosResponse<IServerResponsePayload<IUser>>>{

        return localComApi.get<IServerResponsePayload<IUser>>(ENDPOINTS.USERS.ME, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    }
};