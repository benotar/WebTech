import classes from './BooksPage.module.css';
import {Table, Modal} from 'antd';
import {useBooksStore} from "../../stores/useBooksStore.ts";
import {useEffect, useState} from "react";
import CreateBookForm from "../../components/CreateBookForm/CreateBookForm.tsx";
import IBook from "../../interfaces/entities/IBook.ts";
import {DeleteOutlined, EditOutlined} from "@ant-design/icons";
import UpdateBookForm from "../../components/UpdateBookForm/UpdateBookForm.tsx";

export default function BooksPage() {

    const [isEditing, setIsEditing] = useState<boolean>(false);
    const [editingBook, setEditingBook] = useState<IBook | null>(null);
    const bookStore = useBooksStore();

    const bookTableColumns = [
        {
            key: '1',
            title: 'Id',
            dataIndex: 'id',
        },
        {
            key: '2',
            title: 'Title',
            dataIndex: 'title',
        },
        {
            key: '3',
            title: 'Genre',
            dataIndex: 'genre',
        },
        {
            key: '4',
            title: 'PublicationYear',
            dataIndex: 'publicationYear',
        },
        {
            key: '5',
            title: 'AuthorId',
            dataIndex: 'authorId',
        },
        {
            key: '6',
            title: 'CreatedAt',
            dataIndex: 'createdAt',
        },
        {
            key: '7',
            title: 'UpdatedAt',
            dataIndex: 'updatedAt',
        },
        {
            key: '8',
            title: 'Actions',
            render: (book: IBook) => {
                return (
                    <>
                        <EditOutlined className={classes.editAction}
                                      onClick={async () => {
                                          await onEditBook(book);
                                      }}
                        />
                        <DeleteOutlined
                            onClick={
                                async () => {
                                    await onDeleteBook(book.id);
                                }
                            } className={classes.deleteAction}
                        />
                    </>
                );
            },
        },
    ];

    const onDeleteBook = async (bookId: string) => {

        Modal.confirm({
            title: 'Are you sure, you want to delete this book record?',
            okText: 'Yes',
            okType: 'danger',
            onOk: async () => {
                await bookStore.deleteBook(bookId);
                await fetchBooks();
            }
        });


    }

    const onEditBook = async (book: IBook) => {
        setIsEditing(true);
        setEditingBook({...book});
    }

    const fetchBooks = async () => {
        await bookStore.getBooks();
    }
    useEffect(() => {
        fetchBooks();
    }, []);


    return (
        <div className={classes.booksPageContainer}>
            <CreateBookForm onBookCreated={fetchBooks}/>
            <section className={classes.tableContainer}>
                <Table
                    columns={bookTableColumns}
                    dataSource={bookStore.books}
                    rowKey="id"
                >
                </Table>
            </section>
            <Modal
                title='Edit book'
                open={isEditing}
                onCancel={() => setIsEditing(false)}
                footer={null} // Вимкнути стандартні кнопки "ОК" та "Скасувати"
            >
                {editingBook && (
                    <UpdateBookForm
                        onBookCreated={async () => {
                            await fetchBooks();
                            setIsEditing(false); // Закрити модальне вікно
                        }}
                        editingBook={editingBook}
                    />
                )}
            </Modal>
        </div>
    );
}