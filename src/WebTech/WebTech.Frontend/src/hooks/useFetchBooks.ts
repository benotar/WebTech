import {message} from "antd";
import {useEffect} from "react";
import {IBookStore} from "../interfaces/stores/IBookStore.ts";

const useFetchBooks = (bookStore: IBookStore) => {

    const fetchBooks = async () => {
        try {
            await bookStore.getBooks();
        } catch (error) {
            console.log('Failed to fetch books: ', error);
            message.error('Failed to fetch books. Please try again later.');
        }
    }

    useEffect(() => {
        fetchBooks();
    }, []);


    return {fetchBooks};
};

export default useFetchBooks;