import React from "react";
import "./App.css";
import Login from "./pages/Login";

function App() {
    return (
        <div className="App">
            <nav className="navbar navbar-expand-md navbar-dark bg-dark mb-4">
                <div className="container-fluid">
                    <a className="navbar-brand" href="#">
                        Home
                    </a>
                    <div>
                        <ul className="navbar-nav me-auto mb-2 mb-md-0">
                            <li className="nav-item active">
                                <a className="nav-link active" href="#">
                                    Register
                                </a>
                            </li>
                            <li className="nav-item active">
                                <a className="nav-link active" href="#">
                                    Login
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

            <main className="form-signin w-100 m-auto">
                <Login/>
            </main>
        </div>
    );
}

export default App;
