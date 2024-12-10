import {IAuthorsStore} from "../interfaces/stores/IAuthorsStore.ts";
import {message} from "antd";
import {useEffect} from "react";

const useFetchAuthors = (authorStore: IAuthorsStore) => {

    const fetchAuthors = async () => {
        try{
            await authorStore.getAuthors();
        }catch(error) {
            console.log('Failed to fetch authors: ', error);
            message.error('Failed to fetch authors. Please try again later.');
        }
    }

    useEffect(() => {
        fetchAuthors();
    }, []);

    return {fetchAuthors};
};

export default useFetchAuthors;