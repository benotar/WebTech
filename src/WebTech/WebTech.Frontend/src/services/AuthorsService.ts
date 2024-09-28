import {AxiosResponse} from "axios";
import IServerResponsePayload from "../interfaces/models/response/IServerResponsePayload.ts";
import IAuthor from "../interfaces/entities/IAuthor.ts";
import localNetApi from "../shared/localNetApi.ts";
import {ICreateAuthor} from "../interfaces/models/request/ICreateAuthor.ts";
import {ENDPOINTS} from "../shared/endoints.ts";
import {IUpdateAuthor} from "../interfaces/models/request/IUpdateAuthor.ts";


export default class AuthorsService {
    static async create(params: ICreateAuthor) : Promise<AxiosResponse<IServerResponsePayload<IAuthor>>> {
        return await localNetApi
            .post<IServerResponsePayload<IAuthor>>(ENDPOINTS.AUTHORS.CREATE, params);
    }

    static async update(params: IUpdateAuthor) : Promise<AxiosResponse<IServerResponsePayload<IAuthor>>> {
        return await localNetApi
            .post<IServerResponsePayload<IAuthor>>(`${ENDPOINTS.AUTHORS.UPDATE(params.authorId)}`, params);
    }

    static async delete(authorId: string) : Promise<AxiosResponse<IServerResponsePayload<IAuthor>>> {
        return await localNetApi
            .delete<IServerResponsePayload<IAuthor>>(`${ENDPOINTS.AUTHORS.DELETE(authorId)}`);
    }

    static async getList() : Promise<AxiosResponse<IServerResponsePayload<IAuthor[]>>> {
        return await localNetApi.get<IServerResponsePayload<IAuthor[]>>(ENDPOINTS.AUTHORS.GET);
    }
}