import {Link} from "react-router-dom";
import classes from './Header.module.css';

export default function Header() {
    return(
        <header>
            <nav className={classes.navbar}>
                <Link to='/' className={classes.navbarLogo}>Web Tech</Link>
                <ul className={classes.navbarNav}>
                    <Link to='/authors' className={classes.navItem}>Authors</Link>
                    <Link to='/books' className={classes.navItem}>Books</Link>
                    <Link to='/login' className={classes.navItem}>Login</Link>
                </ul>
            </nav>
        </header>
    );
}