import UserService from "../../services/UserService.ts";

import classes from './UsersPage.module.css';
import {useEffect, useState} from "react";
import IUser from "../../interfaces/entities/IUser.ts";
import {useAuthStore} from "../../store/store.ts";

export default function UsersPage() {

    const [userInfo, setUserInfo] = useState<IUser | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [accessToken, setAccessToken] = useState<string | null>(null);
    const { token, refresh } = useAuthStore();

    const handleGetMe = async () => {

        try{
            console.log('Try get current user info');

            setAccessToken(token);

            if(!accessToken){
                setAccessToken(await refresh());
            }


            const response = await UserService.GetMe(accessToken);

            if(response.data.isSucceed){
                setUserInfo(response.data.data);
            } else {
                setError(response.data.errorCode);
            }
        }catch(error) {
            setError('Failed to get current user info!');
        } finally {
            setLoading(false);
        }
    }




    useEffect(() => {
        handleGetMe();
    }, []);

    if(loading){
        return <p>Loading...</p>
    }

    if(error) {
        return <p>{error}</p>;
    }

    return (
        <div>
            <h1>User Profile</h1>
            <p>Username: {userInfo.userName}</p>
            <p>First Name: {userInfo.firstName}</p>
            <p>Last Name: {userInfo.lastName}</p>
            <p>Address: {userInfo.address}</p>
        </div>
    );
}