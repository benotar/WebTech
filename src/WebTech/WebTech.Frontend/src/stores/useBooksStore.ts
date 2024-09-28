import {create} from "zustand";
import {IBookStore} from "../interfaces/stores/IBookStore.ts";
import BooksService from "../services/BooksService.ts";
import {ICreateBook} from "../interfaces/models/request/ICreateBook.ts";
import {IUpdateBook} from "../interfaces/models/request/IUpdateBook.ts";

export const useBooksStore = create<IBookStore>((set) => ({
    isLoading: false,
    errorCode: null,
    books: [],

    getBooks: async (): Promise<void> => {
        set({isLoading: true});

        const response = await BooksService.get();

        if (response.data.isSucceed) {
            set({books: response.data.data});
        } else {
            set({errorCode: response.data.errorCode});
        }

        set({isLoading: false});
    },
    createBook: async (request: ICreateBook): Promise<void> => {
        set({isLoading: true});

        const response = await BooksService.create(request);

        if(!response.data.isSucceed) {
            set({errorCode: response.data.errorCode});
        }

        set({isLoading: false});
    },
    updateBook: async (request: IUpdateBook): Promise<void> => {
        set({isLoading: true});

        const response = await BooksService.update(request);

        if(!response.data.errorCode) {
            set({errorCode: response.data.errorCode});
        }

        set({isLoading: false});
    },
    deleteBook: async (request: string): Promise<void> => {
        set({isLoading: true});

        const response = await BooksService.delete(request);

        if(!response.data.isSucceed) {
            set({errorCode: response.data.errorCode});
        }

        set({isLoading: false});
    }
}))