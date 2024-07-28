import React from 'react';

const Home = (props: {
    userId: string;
    username: string;
    passwordSalt: string;
    passwordHash: string;
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    address: string;
}) => {
    return (
        <div className="container mt-5">
            <h1 className="text-center">Home</h1>
            <div style={{ marginTop: '30px' }}>
                <h2 className="mb-4 text-center">User profile</h2>
                {props.username ? (
                    <div className="card shadow-sm mx-auto" style={{ width: '400px' }}>
                        <div className="card-body">
                            <h5 className="card-title">{props.username}</h5>
                            <p className="card-text my-font-family"><strong>User ID:</strong> {props.userId}</p>
                            <p className="card-text my-font-family"><strong>Password Salt:</strong> {props.passwordSalt}</p>
                            <p className="card-text my-font-family"><strong>Password Hash:</strong> {props.passwordHash}</p>
                            <p className="card-text my-font-family"><strong>First Name:</strong> {props.firstName}</p>
                            <p className="card-text my-font-family"><strong>Last Name:</strong> {props.lastName}</p>
                            <p className="card-text my-font-family"><strong>Date of Birth:</strong> {props.dateOfBirth}</p>
                            <p className="card-text my-font-family"><strong>Address:</strong> {props.address}</p>
                        </div>
                    </div>
                ) : (
                    <div className="alert alert-warning mt-4" role="alert">
                        You are not logged!
                    </div>
                )}
            </div>
        </div>
    );
};

export default Home;
