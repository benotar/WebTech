import React, {FormEvent, SyntheticEvent, useState} from 'react';
import {Navigate} from "react-router-dom";

const Register = () => {

    const [username, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [birthDate, setBirthDate] = useState('');
    const [address, setAddress] = useState('');
    const [redirectToLogin, setRedirectToLogin] = useState(false);

    const submitForm = async (e: SyntheticEvent) => {
        e.preventDefault();

        const response =  await fetch('http://bg-local.com:5000/User/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                username,
                password,
                confirmPassword,
                firstName,
                lastName,
                birthDate,
                address
            })
        });

        if(response.ok) {
            setRedirectToLogin(true);
        }
    }

    if(redirectToLogin){
        return <Navigate to={'/login'}/>
    }

    return (
        <form onSubmit={submitForm}>
            <h1 className="h3 mb-3 fw-normal font-and-size">Please register</h1>

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
            <div className="form-floating">
                <input
                    type="password"
                    className="form-control"
                    placeholder="Confirm Password"
                    required
                    onChange={e => setConfirmPassword(e.target.value)}
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
                    onChange={e => setBirthDate(e.target.value)}
                />
            </div>
            <div className="form-floating">
                <input
                    type="text"
                    className="form-control"
                    placeholder="Address"
                    required
                    onChange={e => setAddress(e.target.value)}
                />
            </div>
            <button
                className="btn btn-primary w-100 py-2 font-and-size"
                type="submit"
            >
                Register
            </button>
        </form>
    );
};

export default Register;