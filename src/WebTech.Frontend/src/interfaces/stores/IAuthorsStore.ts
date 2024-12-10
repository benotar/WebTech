import IAuthor from "../entities/IAuthor.ts";
import {ICreateAuthor} from "../models/request/ICreateAuthor.ts";
import {IUpdateAuthor} from "../models/request/IUpdateAuthor.ts";

export interface IAuthorsStore {
    isLoading: boolean;
    errorCode: string | null;
    authors: IAuthor[];

    getAuthors() :  Promise<void>;
    createAuthor(request: ICreateAuthor) :  Promise<void>;
    updateAuthor(request: IUpdateAuthor) :  Promise<void>;
    deleteAuthor(authorId: string) :  Promise<void>;
}