import classes from './UsersPage.module.css';
import { useEffect, useState } from "react";
import IUser from "../../interfaces/entities/IUser";
import { useAuthStore } from "../../stores/useAuthStore";
import { Spin, Table} from "antd";

export default function UsersPage() {
    const [userInfo, setUserInfo] = useState<IUser | null>(null);
    const { user } = useAuthStore();

    useEffect(() => {
        setUserInfo(user);
    }, [user]);


    const userInfoColumns = [
        { title: 'Id', dataIndex: 'id', key: 'id' },
        { title: 'UserName', dataIndex: 'userName', key: 'userName' },
        { title: 'FirstName', dataIndex: 'firstName', key: 'firstName' },
        { title: 'LastName', dataIndex: 'lastName', key: 'lastName' },
        { title: 'BirthDate', dataIndex: 'birthDate', key: 'birthDate' },
        { title: 'Address', dataIndex: 'address', key: 'address' },
        { title: 'Created At', dataIndex: 'createdAt', key: 'createdAt' },
        { title: 'Updated At', dataIndex: 'updatedAt', key: 'updatedAt' },
    ];

    return (
        <div className={classes.container}>
            <h1 className={classes.title}>User Profile</h1>
            {userInfoColumns ? (
                <Table
                    columns={userInfoColumns}
                    dataSource={[userInfo]}
                    rowKey="id"
                />
            ) : (
                <Spin tip="Loading..."/>
            )}
        </div>
    );
}