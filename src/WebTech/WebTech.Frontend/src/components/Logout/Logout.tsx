import {FC, useEffect} from "react";
import {useAuthStore} from "../../store/store.ts";

const Logout: FC = () => {
    const {logout} = useAuthStore();

    const handleLogout = async () => {
        await logout();
    }

    useEffect(() => {
        handleLogout();
    }, [logout]);

    return (
        <>
            You have been logged out!
        </>
    );
}

export default Logout;