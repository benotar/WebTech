import {AxiosResponse} from "axios";
import IServerResponsePayload from "../interfaces/models/response/IServerResponsePayload.ts";
import localNetApi from "../shared/localNetApi.ts";
import {ENDPOINTS} from "../shared/endoints.ts";
import {ICreateBook} from "../interfaces/models/request/ICreateBook.ts";
import {IUpdateBook} from "../interfaces/models/request/IUpdateBook.ts";
import IBook from "../interfaces/entities/IBook.ts";


export default class BooksService {
    static async create(params: ICreateBook) : Promise<AxiosResponse<IServerResponsePayload<IBook>>> {
        return await localNetApi
            .post<IServerResponsePayload<IBook>>(ENDPOINTS.BOOKS.CREATE, params);
    }

    static async update(params: IUpdateBook) : Promise<AxiosResponse<IServerResponsePayload<IBook>>> {
        return await localNetApi
            .post<IServerResponsePayload<IBook>>(`${ENDPOINTS.BOOKS.UPDATE}/${params.bookId}`, params);
    }

    static async delete(bookId: string) : Promise<AxiosResponse<IServerResponsePayload<IBook>>> {
        return await localNetApi
            .post<IServerResponsePayload<IBook>>(`${ENDPOINTS.BOOKS.DELETE}/${bookId}`);
    }

    static async getList() : Promise<AxiosResponse<IServerResponsePayload<IBook[]>>> {
        return await localNetApi.get<IServerResponsePayload<IBook[]>>(ENDPOINTS.BOOKS.GET);
    }
}