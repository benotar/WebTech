import {Link, useLocation} from "react-router-dom";
import {Layout, Menu} from 'antd';
import classes from './Header.module.css';
import {useAuthStore} from "../../stores/useAuthStore.ts";
import {FC} from "react";

const {Header} = Layout;

const MyHeader: FC = () => {
    const {isAuthenticated} = useAuthStore();
    const location = useLocation();

    const menuItems = isAuthenticated
        ? [
            {key: '/authors', label: 'Authors'},
            {key: '/books', label: 'Books'},
            {key: '/me', label: 'About me'},
            {key: '/logout', label: 'Logout'},
        ]
        : [
            {key: '/login', label: 'Login'},
            {key: '/register', label: 'Register'},
        ];

    return (
        <Header>
            <div className={classes.logo}>
                <Link to='/' className={classes.navbarLogo}>Web Tech</Link>
            </div>
            <Menu
                className={classes.navbarNav}
                theme='dark'
                mode="horizontal"
                selectedKeys={[location.pathname]}
            >

                {menuItems.map(item => (
                    <Menu.Item key={item.key}>
                        <Link to={item.key}>{item.label}</Link>
                    </Menu.Item>
                ))}
            </Menu>
        </Header>
    );
}

export default MyHeader;
