import React from 'react';
import NavAuthors from "../components/NavAuthors";
import {Link} from "react-router-dom";

const Books = () => {
    return (
        <div className="Books">
            <nav className="navbar navbar-expand-md navbar-dark bg-dark mb-4">
                <div className="container-fluid">
                    <ul className="navbar-nav me-auto mb-2 mb-md-0">
                        <li className="nav-item">
                            <Link to={'/createBook'} className="nav-link font-and-size-content">
                                Create Book
                            </Link>
                        </li>
                    </ul>
                </div>
            </nav>

        </div>
    );
};

export default Books;