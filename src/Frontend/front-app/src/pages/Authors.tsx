import React, { useEffect, useState } from 'react';
import NavAuthors from "../components/NavAuthors";
import {Link} from "react-router-dom";


const Authors = () => {

    return (
        <div className="Authors">
            <NavAuthors />
            <br /><br />
            <ul className="navbar-nav me-auto mb-2 mb-md-0">
                <li className="nav-item">
                    <Link to={'/createAuthor'} className="nav-link font-and-size-content">
                        Create Author
                    </Link>
                </li>
                <li className="nav-item">
                    <Link to={'/updateAuthor'} className="nav-link font-and-size-content">
                        Update Author
                    </Link>
                </li>
            </ul>
        </div>
    );
};

export default Authors;
