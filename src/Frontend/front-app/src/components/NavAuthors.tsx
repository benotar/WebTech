import React from 'react';
import {Link} from "react-router-dom";

const Nav = () => {

    const logout = async () => {
        const response = await fetch('http://api.bg-local.net:8000/Authors/logout', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include',
        });
    }

    return (
        <nav className="navbar navbar-expand-md navbar-dark bg-dark mb-4">
            <div className="container-fluid">
                <ul className="navbar-nav me-auto mb-2 mb-md-0">
                    <li className="nav-item">
                        <Link to={'/loginAuthor'} className="nav-link font-and-size-content">
                            Login
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to={'/authors'} className="nav-link font-and-size-content" onClick={logout}>
                            Logout
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to={'/books'} className="nav-link font-and-size-content">
                            Books
                        </Link>
                    </li>
                </ul>
            </div>
        </nav>
    );
};

export default Nav;
