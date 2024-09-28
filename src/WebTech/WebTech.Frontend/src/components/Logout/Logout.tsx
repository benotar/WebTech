import {FC, useEffect} from "react";
import {useAuthStore} from "../../stores/useAuthStore.ts";

import {useNavigate} from "react-router-dom";


const Logout: FC = () => {
    const {logout} = useAuthStore();

    const navigate = useNavigate();


    const handleLogout = async () => {
        await logout();
    }

    useEffect(() => {
        handleLogout();
    }, [logout]);

    return (
        <>
            {navigate('/')}
        </>
    );
}

export default Logout;