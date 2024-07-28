import React from 'react';

const Login = () => {
    return (
        <form>
            <h1 className="h3 mb-3 fw-normal font-and-size">Please sign in</h1>

            <div className="form-floating">
                <input
                    type="text"
                    className="form-control"
                    placeholder="Username"
                    required
                />
            </div>
            <div className="form-floating">
                <input
                    type="password"
                    className="form-control"
                    placeholder="Password"
                    required
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