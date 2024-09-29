import React from 'react';
import classes from './HomePage.module.css';
import { useAuthStore } from "../../stores/useAuthStore.ts";

const HomePage: React.FC = () => {
    const { isAuthenticated, user } = useAuthStore();

    return (
        <div className={classes.container}>

            {isAuthenticated ? (
                <>
                    <h1 className={classes.greeting}>Hello, {user?.userName}!</h1>
                    <h2 className={classes.title}>Welcome to Web Tech!</h2>
                    <p className={classes.desc}>
                        Explore the latest features and enjoy your experience!
                    </p>
                </>
            ) : (
                <>
                    <h1 className={classes.greeting}>Hello, Guest!</h1>
                    <h2 className={classes.title}>Welcome to Web Tech!</h2>
                    <p className={classes.desc}>
                        To get started, log in to an existing account or create a new one.
                    </p>
                </>
            )}
        </div>
    );
};

export default HomePage;