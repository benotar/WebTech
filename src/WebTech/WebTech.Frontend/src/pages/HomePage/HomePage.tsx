import classes from './HomePage.module.css';
import { useAuthStore } from "../../stores/useAuthStore.ts";

export default function HomePage() {
    const { isAuthenticated, user } = useAuthStore();

    return (
        <div className={classes.container}>
            <h2 className={classes.greeting}>Welcome to Web Tech!</h2>

            {isAuthenticated ? (
                <>
                    <p className={classes.greeting}>Hello, {user?.userName}!</p>
                    <p className={classes.desc}>
                        Explore the latest features and enjoy your experience!
                    </p>
                </>
            ) : (
                <>
                    <p className={classes.greeting}>Hello, Guest!</p>
                    <p className={classes.desc}>
                        To get started, log in to an existing account or create a new one
                    </p>
                </>
            )}


        </div>
    );
}