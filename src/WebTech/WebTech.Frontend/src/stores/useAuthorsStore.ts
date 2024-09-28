import {create} from "zustand";

import AuthorsService from "../services/AuthorsService.ts";
import {ICreateAuthor} from "../interfaces/models/request/ICreateAuthor.ts";
import {IUpdateAuthor} from "../interfaces/models/request/IUpdateAuthor.ts";
import {IAuthorsStore} from "../interfaces/stores/IAuthorsStore.ts";

export const useAuthorsStore = create<IAuthorsStore>((set) => ({
    isLoading: false,
    errorCode: null,
    authors: [],

    getAuthors: async (): Promise<void> => {
        set({isLoading: true});

        const response = await AuthorsService.getList();

        if (response.data.isSucceed) {
            set({authors: response.data.data});
        } else {
            set({errorCode: response.data.errorCode});
        }

        set({isLoading: false});
    },
    createAuthor: async (request: ICreateAuthor): Promise<void> => {
        set({isLoading: true});

        const response = await AuthorsService.create(request);

        if(!response.data.isSucceed) {
            set({errorCode: response.data.errorCode});
        }

        set({isLoading: false});
    },
    updateAuthor: async (request: IUpdateAuthor): Promise<void> => {
        set({isLoading: true});

        const response = await AuthorsService.update(request);

        if(!response.data.isSucceed) {
            set({errorCode: response.data.errorCode});
        }

        set({isLoading: false});
    },
    deleteAuthor: async (request: string): Promise<void> => {
        set({isLoading: true});

        const response = await AuthorsService.delete(request);

        if(!response.data.isSucceed) {
            set({errorCode: response.data.errorCode});
        }

        set({isLoading: false});
    }
}));