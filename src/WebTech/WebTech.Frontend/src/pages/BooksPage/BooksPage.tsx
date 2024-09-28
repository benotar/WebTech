//import classes from './BooksPage.module.css';

import {Table, } from 'antd';
import {useBooksStore} from "../../stores/useBooksStore.ts";
import {useEffect} from "react";
import CreateBookForm from "../../components/CreateBookForm/CreateBookForm.tsx";

export default function BooksPage() {

    const columns = [
        {
            key: '1',
            title: 'Id',
            dataIndex: 'id'
        },
        {
            key: '2',
            title: 'Title ',
            dataIndex: 'title'
        },
        {
            key: '3',
            title: 'Genre',
            dataIndex: 'genre'
        },
        {
            key: '4',
            title: 'PublicationYear ',
            dataIndex: 'publicationYear'
        },
        {
            key: '5',
            title: 'AuthorId ',
            dataIndex: 'authorId'
        },
        {
            key: '6',
            title: 'CreatedAt ',
            dataIndex: 'createdAt'
        },
        {
            key: '7',
            title: 'UpdatedAt ',
            dataIndex: 'updatedAt'
        },
    ]




    const bookStore = useBooksStore();

    const fetchBooks = async () => {
        await bookStore.getBooks();
    }
    useEffect(() => {
        fetchBooks();
    }, []);


    return (
        <>

            <CreateBookForm onBookCreated={fetchBooks}/>

            <Table
                columns={columns}
                dataSource={bookStore.books}>

            </Table>

        </>
    );
}