import React, {SyntheticEvent, useState} from 'react';
import {Navigate} from "react-router-dom";

const UpdateAuthor = () => {
    
    const [authorId, setAuthorId] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    const [redirect, setRedirect] = useState(false);


    const submitForm = async (e: SyntheticEvent) => {
        e.preventDefault();

        const response = await fetch('http://api.bg-local.net:8000/Authors/update-author', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                authorId,
                firstName,
                lastName,
                dateOfBirth,
            }),
            credentials: 'include'
        });

        console.log(firstName,
            lastName,
            dateOfBirth);

        if (response.ok) {

            alert('The author has been successfully updated!');

            setRedirect(true);
        } else {
            alert('Error updating author!');
        }
    }

    if (redirect) {
        return <Navigate to={'/authors'}/>
    }
   
    return (
        <div>
            <form onSubmit={submitForm}>
                <h1 className="h3 mb-3 fw-normal font-and-size">Update Author</h1>

                <div className="form-floating">
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Author Id"
                        required
                        onChange={e => setAuthorId(e.target.value)}
                    />
                </div>

                <div className="form-floating">
                    <input
                        type="text"
                        className="form-control"
                        placeholder="First Name"
                        required
                        onChange={e => setFirstName(e.target.value)}
                    />
                </div>
                <div className="form-floating">
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Last Name"
                        required
                        onChange={e => setLastName(e.target.value)}
                    />
                </div>
                <div className="form-floating">
                    <input
                        type="date"
                        className="form-control"
                        placeholder="Birth date"
                        required
                        onChange={e => setDateOfBirth(e.target.value)}
                    />
                </div>
                <button className="btn btn-primary w-100 py-2 font-and-size" type="submit">Update Author</button>
            </form>
        </div>
    );
};

export default UpdateAuthor;