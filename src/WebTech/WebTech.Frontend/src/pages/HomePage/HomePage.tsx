import classes from './HomePage.module.css';
import {useAuthStore} from "../../stores/useAuthStore.ts";

export default function HomePage() {

    const {isAuthenticated, user} = useAuthStore();

    return (
        <div className={classes.container}>
            <h2 className={classes.greeting}>Welcome on Web Tech</h2>

            {/* Умовне рендерування для автентифікованих користувачів */}
            {isAuthenticated ? (
                <p className={classes.greeting}>Hello, {user?.userName}!</p>
            ) : (
                <p className={classes.greeting}>Hello, Guest!</p>
            )}

            <p className={classes.desc}>
                Some text
            </p>
        </div>
    );
}
