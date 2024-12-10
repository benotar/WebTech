import IBook from "../entities/IBook.ts";
import {ICreateBook} from "../models/request/ICreateBook.ts";
import {IUpdateBook} from "../models/request/IUpdateBook.ts";

export interface IBookStore {
    isLoading: boolean;
    errorCode: string | null;
    books: IBook[];

    getBooks() :  Promise<void>;
    createBook(request: ICreateBook) :  Promise<void>;
    updateBook(request: IUpdateBook) :  Promise<void>;
    deleteBook(bookId: string) :  Promise<void>;
}