import React, {SyntheticEvent, useState} from 'react';
import {Navigate} from "react-router-dom";

const LoginAuthor = () => {

    const [redirect, setRedirect] = useState(false);

    const submit = async (e:SyntheticEvent) => {
        e.preventDefault();

        const response = await fetch('http://api.bg-local.net:8000/Authors/login', {
            method: 'POST',
            headers: {'Content-Type': 'application/json',},
            credentials: 'include',
        });

        if (response.ok) {
            alert('Login author successful!');

            setRedirect(true);

        } else {
            alert('Login failed!');
        }
    }

    if(redirect){
        return <Navigate to={'/authors'}/>
    }

    return (
        <form onSubmit={submit}>
            <button className="btn btn-primary w-100 py-2 font-and-size" type="submit">
                Click me to enter
            </button>
        </form>
    );
};

export default LoginAuthor;