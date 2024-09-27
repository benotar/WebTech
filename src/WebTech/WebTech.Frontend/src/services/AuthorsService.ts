import {AxiosResponse} from "axios";
import IServerResponsePayload from "../interfaces/models/response/IServerResponsePayload.ts";
import IAuthor from "../interfaces/entities/IAuthor.ts";
import localNetApi from "../shared/localNetApi.ts";
import {ICreateOrUpdateAuthor} from "../interfaces/models/request/ICreateOrUpdateAuthor.ts";
import {ENDPOINTS} from "../shared/endoints.ts";


export default class AuthorsService {
    static async create(params: ICreateOrUpdateAuthor) : Promise<AxiosResponse<IServerResponsePayload<IAuthor>>> {
        return await localNetApi
            .post<IServerResponsePayload<IAuthor>>(ENDPOINTS.AUTHORS.CREATE, params);
    }

    static async update(authorId: string, params: ICreateOrUpdateAuthor) : Promise<AxiosResponse<IServerResponsePayload<IAuthor>>> {
        return await localNetApi
            .post<IServerResponsePayload<IAuthor>>(`${ENDPOINTS.AUTHORS.UPDATE}/${authorId}`, params);
    }

    static async delete(authorId: string) : Promise<AxiosResponse<IServerResponsePayload<IAuthor>>> {
        return await localNetApi
            .post<IServerResponsePayload<IAuthor>>(`${ENDPOINTS.AUTHORS.DELETE}/${authorId}`);
    }

    static async getList() : Promise<AxiosResponse<IServerResponsePayload<IAuthor[]>>> {
        return await localNetApi.get<IServerResponsePayload<IAuthor[]>>(ENDPOINTS.AUTHORS.GET);
    }
}