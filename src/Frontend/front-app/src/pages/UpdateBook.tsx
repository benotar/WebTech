import React, {SyntheticEvent, useState} from 'react';
import {Navigate} from "react-router-dom";

const UpdateBook = () => {

    const [bookId, setBookId] = useState('');
    const [name, setName] = useState('');
    const [publicAt, setPublicAt] = useState('');
    const [genre, setGenre] = useState('');
    const [authorId, setAuthorId] = useState('');
    const [redirect, setRedirect] = useState(false);



    const submitForm = async (e: SyntheticEvent) => {
        e.preventDefault();

        const response = await fetch('http://api.bg-local.net:8000/Authors/update-book', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                bookId,
                name,
                publicAt,
                genre,
                authorId
            }),
            credentials: 'include'
        });


        if (response.ok) {

            alert('The book has been successfully updated!');

            setRedirect(true);
        } else {
            alert('Error updating book!');
        }
    }

    if (redirect) {
        return <Navigate to={'/books'}/>
    }

    return (
        <div>
            <form onSubmit={submitForm}>
                <h1 className="h3 mb-3 fw-normal font-and-size">Update Author</h1>

                <div className="form-floating">
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Book Id"
                        required
                        onChange={e => setBookId(e.target.value)}
                    />
                </div>

                <div className="form-floating">
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Name"
                        required
                        onChange={e => setName(e.target.value)}
                    />
                </div>
                <div className="form-floating">
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Public At"
                        required
                        onChange={e => setPublicAt(e.target.value)}
                    />
                </div>
                <div className="form-floating">
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Genre"
                        required
                        onChange={e => setGenre(e.target.value)}
                    />
                </div>
                <div className="form-floating">
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Author Id"
                        required
                        onChange={e => setAuthorId(e.target.value)}
                    />
                </div>
                <button className="btn btn-primary w-100 py-2 font-and-size" type="submit">Update Book</button>
            </form>
        </div>
    );
};

export default UpdateBook;