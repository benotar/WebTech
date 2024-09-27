import {Link} from "react-router-dom";
import classes from './Header.module.css';
import {useAuthStore} from "../../store/store.ts";

export default function Header() {

    const {isAuthenticated} = useAuthStore();


    return (
        <header>
            <nav className={classes.navbar}>
                <Link to='/' className={classes.navbarLogo}>Web Tech</Link>

                <ul className={classes.navbarNav}>

                    {isAuthenticated ? (
                        <>
                            <Link to='/authors' className={classes.navItem}>Authors</Link>
                            <Link to='/books' className={classes.navItem}>Books</Link>
                            <Link to='/logout' className={classes.navItem}>Logout</Link>
                        </>
                    ) : (
                        <Link to='/login' className={classes.navItem}>Login</Link>
                    )}


                </ul>
            </nav>
        </header>
    );
}