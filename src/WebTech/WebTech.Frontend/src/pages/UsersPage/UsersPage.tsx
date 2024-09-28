//import classes from './UsersPage.module.css';
import {useEffect, useState} from "react";
import IUser from "../../interfaces/entities/IUser.ts";
import {useAuthStore} from "../../stores/useAuthStore.ts";


export default function UsersPage() {

    const [userInfo, setUserInfo] = useState<IUser | null>(null);
    const {user} = useAuthStore();

    useEffect(() => {
        setUserInfo(user);
    }, []);



    return (
        <div>
            <h1>User Profile</h1>
            {userInfo ? (
                <>
                    <p>Username: {userInfo.userName}</p>
                    <p>First Name: {userInfo.firstName}</p>
                    <p>Last Name: {userInfo.lastName}</p>
                    <p>Address: {userInfo.address}</p>
                </>
            ) : (
                <p>Loading...</p>
            )}
        </div>
    );
}