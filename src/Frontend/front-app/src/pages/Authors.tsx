import React from 'react';
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Nav from "../components/Nav";
import Home from "./Home";
import Login from "./Login";
import Register from "./Register";
import NavAuthors from "../components/NavAuthors";
import LoginAuthor from "./LoginAuthor";

const Authors = () => {
    return (

        <div className="Authors">
            <NavAuthors/>

        </div>

    );
};

export default Authors;