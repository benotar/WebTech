import classes from './BooksPage.module.css';
import {Table, Modal, message} from 'antd';
import {useBooksStore} from "../../stores/useBooksStore.ts";
import {FC, useState} from "react";
import CreateBookForm from "../../components/CreateBookForm/CreateBookForm.tsx";
import IBook from "../../interfaces/entities/IBook.ts";
import {DeleteOutlined, EditOutlined} from "@ant-design/icons";
import UpdateBookForm from "../../components/UpdateBookForm/UpdateBookForm.tsx";
import useFetchBooks from "../../hooks/useFetchBooks.ts";

const BooksPage: FC = () => {
    const [isEditing, setIsEditing] = useState<boolean>(false);
    const [editingBook, setEditingBook] = useState<IBook | null>(null);
    const bookStore = useBooksStore();
    const {fetchBooks} = useFetchBooks(bookStore);

    const booksTableColumns = [
        {title: 'Id', dataIndex: 'id', key: 'id'},
        {title: 'Title', dataIndex: 'title', key: 'title'},
        {title: 'Genre', dataIndex: 'genre', key: 'genre'},
        {title: 'Publication Year', dataIndex: 'publicationYear', key: 'publicationYear'},
        {title: 'Author Id', dataIndex: 'authorId', key: 'authorId'},
        {title: 'Created At', dataIndex: 'createdAt', key: 'createdAt'},
        {title: 'Updated At', dataIndex: 'updatedAt', key: 'updatedAt'},
        {
            title: 'Actions',
            key: 'actions',
            render: (book: IBook) => (
                <>
                    <EditOutlined className={classes.editAction} onClick={() => onEditBook(book)}/>
                    <DeleteOutlined className={classes.deleteAction} onClick={() => onDeleteBook(book.id)}/>
                </>
            ),
        },
    ];

    const onDeleteBook = async (bookId: string) => {

        Modal.confirm({
            title: 'Are you sure, you want to delete this book record?',
            okText: 'Yes',
            okType: 'danger',
            onOk: async () => {
                try {
                    await bookStore.deleteBook(bookId);
                    await fetchBooks();
                    message.success('Book record deleted successfully!');
                } catch (error) {
                    console.error('Failed to delete book:', error);
                    message.error('Failed to delete book record. Please try again later.');
                }
            }
        });
    }

    const onEditBook = async (book: IBook) => {
        setIsEditing(true);
        setEditingBook({...book});
    }


    return (
        <div className={classes.booksPageContainer}>
            <CreateBookForm onBookCreated={fetchBooks}/>
            <section className={classes.tableContainer}>
                <Table
                    columns={booksTableColumns}
                    dataSource={bookStore.books}
                    rowKey="id"
                />
            </section>
            <Modal
                title='Edit book'
                open={isEditing}
                onCancel={() => setIsEditing(false)}
                footer={null}
            >
                {editingBook && (
                    <UpdateBookForm
                        onBookUpdated={
                            async () => {
                                await fetchBooks();
                                setIsEditing(false);
                            }}
                        editingBook={editingBook}
                    />
                )}
            </Modal>
        </div>
    );
}

export default BooksPage;