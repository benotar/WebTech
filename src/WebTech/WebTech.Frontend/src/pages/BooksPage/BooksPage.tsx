//import classes from './BooksPage.module.css';


import {useBooksStore} from "../../stores/useBooksStore.ts";
import {useEffect, useState} from "react";

export default function BooksPage() {

    const [title, setTitle] = useState('');
    const [genre, setGenre] = useState('');
    const [publicationYear, setPublicationYear] = useState(0);
    const [authorFirstName, setAuthorFirstName] = useState('');
    const [authorLastName, setAuthorLastName] = useState('');


    const bookStore = useBooksStore();

    const fetchBooks = async () => {
        await bookStore.getBooks();
    }
    useEffect(() => {
        fetchBooks();
    }, []);


    const handleCreateBook = async () => {
        await bookStore.createBook({
            title: title,
            genre: genre,
            publicationYear: publicationYear,
            authorFirstName: authorFirstName,
            authorLastName: authorLastName
        });

        clearForm();

        await fetchBooks();
    }



    const handleDeleteBook = async (bookId: string) => {
        try {
            await bookStore.deleteBook(bookId);

            await fetchBooks();
        } catch (error) {
            console.log('Error deleting book: ', error);
        }
    }

    const clearForm = () => {
        setTitle('');
        setGenre('');
        setPublicationYear(0);
        setAuthorFirstName('');
        setAuthorLastName('');
    }

    return (
        <>
            <h1>Books</h1>

            <ul>
                {bookStore.books.map(book => (
                    <li key={book.id}>
                        <p>ID : {book.id}</p>
                        <p>Title: {book.title}, author id {book.authorId}</p>
                        <button onClick={() => handleDeleteBook(book.id)}>Delete</button>
                    </li>
                ))}
            </ul>


            {/*    TEST*/}
            <div>
                <input
                    onChange={e => setTitle(e.target.value)}
                    value={title}
                    type="text"
                    placeholder="Title"
                />
                <input
                    onChange={e => setGenre(e.target.value)}
                    value={genre}
                    type="text"
                    placeholder="Genre"
                />
                <input
                    onChange={e => setPublicationYear(e.target.valueAsNumber)}
                    value={publicationYear}
                    type="number"
                    placeholder="Publication Year"
                />
                <input
                    onChange={e => setAuthorFirstName(e.target.value)}
                    value={authorFirstName}
                    type="text"
                    placeholder="Author FirstName"
                />
                <input
                    onChange={e => setAuthorLastName(e.target.value)}
                    value={authorLastName}
                    type="text"
                    placeholder="Genre"
                />

                <button onClick={handleCreateBook}>Create</button>


            </div>

        </>
    );
}