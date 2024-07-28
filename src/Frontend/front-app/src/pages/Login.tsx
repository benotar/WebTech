import React, {SyntheticEvent, useState} from 'react';
import Nav from "../components/Nav";
import {Navigate} from "react-router-dom";




const Login = (props: { setUserName: (username: string) => void }) => {

    const [username, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [redirect, setRedirect] = useState(false);


    const submit = async (e:SyntheticEvent) => {
        e.preventDefault();

        const response = await fetch('http://bg-local.com:5000/User/login', {
            method: 'POST',
            headers: {'Content-Type': 'application/json',},
            credentials: 'include',
            body: JSON.stringify({
                username,
                password,
            })
        });

        if (response.ok) {
            alert('Login successful!');

            const content = await response.json();

            setRedirect(true);

            props.setUserName(content.username);
        } else {
            alert('Login failed!');
        }
    }

    if(redirect){
        return <Navigate to={'/'}/>
    }

    return (
        <form onSubmit={submit}>
            <h1 className="h3 mb-3 fw-normal font-and-size">Please sign in</h1>

            <div className="form-floating">
                <input
                    type="text"
                    className="form-control"
                    placeholder="Username"
                    required
                    onChange={e => setUserName(e.target.value)}
                />
            </div>
            <div className="form-floating">
                <input
                    type="password"
                    className="form-control"
                    placeholder="Password"
                    required
                    onChange={e => setPassword(e.target.value)}
                />
            </div>
            <button
                className="btn btn-primary w-100 py-2 font-and-size"
                type="submit"
            >
                Sign in
            </button>
        </form>
    );
};

export default Login;