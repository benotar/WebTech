import React, {useEffect, useState} from "react";
import "./App.css";
import Login from "./pages/Login";
import Nav from "./components/Nav";
import {BrowserRouter, Routes, Route} from "react-router-dom";
import Home from "./pages/Home";
import Register from "./pages/Register";
import Authors from "./pages/Authors";
import LoginAuthor from "./pages/LoginAuthor";
import CreateAuthor from "./pages/CreateAuthor";
import Books from "./pages/Books";
import CreateBook from "./pages/CreateBook";
import UpdateBook from "./pages/UpdateBook";
import UpdateAuthor from "./pages/UpdateAuthor";

function App() {

    const[user, setUser] = useState({
        userId: '',
        username: '',
        passwordSalt: '',
        passwordHash: '',
        firstName: '',
        lastName: '',
        dateOfBirth: '',
        address: ''
    });

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const response = await fetch('http://bg-local.com:5000/User/me', {
                    headers: {'content-type': 'application/json'},
                    credentials: 'include'
                });

                if (response.ok) {
                    const content = await response.json();
                    console.log('User data:', content);

                    setUser({
                        userId: content.user.id,
                        username: content.user.username,
                        passwordSalt: content.user.passwordSalt,
                        passwordHash: content.user.passwordHash,
                        firstName: content.user.firstName,
                        lastName: content.user.lastName,
                        dateOfBirth: content.user.dateOfBirth,
                        address: content.user.address
                    })

                } else {
                    console.error('Failed to fetch user data:', response.statusText);
                }
            } catch (error) {
                console.error('An error occurred while fetching user data:', error);
            }
        };

        fetchUserData();
    }, []);

    return (
        <div className="App">
            <BrowserRouter>
                <Nav userName={user.username} setUserName={(username) => setUser({...user, username})}/>
                <main className="form-signin w-100 m-auto">
                    <Routes>
                        <Route path="/" element={<Home {...user} />} />
                        <Route path="/authors" element={<Authors/>} />
                        <Route path="/loginAuthor" element={<LoginAuthor/>} />
                        <Route path="/createAuthor" element={<CreateAuthor/>} />
                        <Route path="/updateAuthor" element={<UpdateAuthor/>} />
                        <Route path="/books" element={<Books/>} />
                        <Route path="/createBook" element={<CreateBook/>} />
                        <Route path="/updateBook" element={<UpdateBook/>} />
                        <Route path="/login" element={<Login setUserName={(username) => setUser({ ...user, username })} />} />
                        <Route path="/register" element={<Register/>}/>
                    </Routes>
                </main>
            </BrowserRouter>
        </div>
    );
}

export default App;
