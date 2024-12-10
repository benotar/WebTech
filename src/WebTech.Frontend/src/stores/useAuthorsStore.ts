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

        try {
            const response = await AuthorsService.getList();

            if (response.data.isSucceed) {
                set({authors: response.data.data});
            }else {
                set({errorCode: response.data.errorCode});
                throw new Error(`Error fetching authors: ${response.data.errorCode}`);
            }
        }catch(error){
            console.log('Failed to fetch authors:', error);
            set({ errorCode: 'Failed to fetch authors' });
        }
        finally {
            set({isLoading: false});
        }

    },
    createAuthor: async (request: ICreateAuthor): Promise<void> => {
        set({isLoading: true});

        try {
            const response = await AuthorsService.create(request);

            if (!response.data.isSucceed) {
                set({errorCode: response.data.errorCode});

                throw new Error(`Error creating author: ${response.data.errorCode}`);
            }
        }catch(error){
            console.log('Failed to create author:', error);
            set({ errorCode: 'Failed to create' });
            throw error;
        }
        finally {
            set({isLoading: false});
        }
    },
    updateAuthor: async (request: IUpdateAuthor): Promise<void> => {
        set({isLoading: true});

        try{
            const response = await AuthorsService.update(request);

            if (!response.data.isSucceed) {
                set({errorCode: response.data.errorCode});
                throw new Error(`Error updating author: ${response.data.errorCode}`);
            }
        }catch(error){
            console.log('Failed to update author:', error);
            set({ errorCode: 'Failed to update author' });
            throw error;
        }finally {
            set({ isLoading: false });
        }
    },
    deleteAuthor: async (request: string): Promise<void> => {
        set({isLoading: true});

       try{
           const response = await AuthorsService.delete(request);

           if (!response.data.isSucceed) {
               set({errorCode: response.data.errorCode});
               throw new Error(`Error deleting author: ${response.data.errorCode}`);
           }
       }catch (error) {
           console.error('Failed to delete author:', error);
           set({ errorCode: 'Failed to delete author' });
           throw error;
       } finally {
           set({ isLoading: false });
       }
    }
}));