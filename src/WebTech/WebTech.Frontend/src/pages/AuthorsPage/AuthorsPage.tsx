import classes from './AuthorsPage.module.css';
import {FC, useState} from "react";
import {useAuthorsStore} from "../../stores/useAuthorsStore.ts";
import IAuthor from "../../interfaces/entities/IAuthor.ts";
import {DeleteOutlined, EditOutlined} from "@ant-design/icons";
import {message, Modal, Table} from "antd";
import CreateAuthorForm from "../../components/CreateAuthorForm/CreateAuthorForm.tsx";
import useFetchAuthors from "../../hooks/useFetchAuthors.ts";
import UpdateAuthorForm from "../../components/UpdateAuthorForm/UpdateAuthorForm.tsx";

const AuthorsPage: FC = () => {
    const [isEditing, setIsEditing] = useState<boolean>(false);
    const [editingAuthor, setEditingAuthor] = useState<IAuthor | null>(null);
    const authorsStore = useAuthorsStore();
    const {fetchAuthors} = useFetchAuthors(authorsStore);

    const authorsTableColumns = [
        {title: 'Id', dataIndex: 'id', key: 'id'},
        {title: 'FirstName ', dataIndex: 'firstName', key: 'firstName'},
        {title: 'LastName ', dataIndex: 'lastName', key: 'lastName'},
        {title: 'BirthDate ', dataIndex: 'birthDate', key: 'birthDate'},
        {title: 'Created At', dataIndex: 'createdAt', key: 'createdAt'},
        {title: 'Updated At', dataIndex: 'updatedAt', key: 'updatedAt'},
        {
            title: 'Actions',
            key: 'actions',
            render: (author: IAuthor) => (
                <>
                    <EditOutlined className={classes.editAction} onClick={() => onEditAuthor(author)}/>
                    <DeleteOutlined className={classes.deleteAction} onClick={() => onDeleteAuthor(author.id)}/>
                </>
            ),
        },
    ];

    const onDeleteAuthor = async (authorId: string): Promise<void> => {
        Modal.confirm({
            title: 'Are you sure, you want to delete this author record?',
            okText: 'Yes',
            okType: 'danger',
            onOk: async () => {
                try {
                    await authorsStore.deleteAuthor(authorId);
                    await fetchAuthors();
                    message.success('Author record deleted successfully!');
                } catch (error) {
                    console.error('Failed to delete author:', error);
                    message.error('Failed to delete author record. Please try again later.');
                }
            }
        });
    }

    const onEditAuthor = async (author: IAuthor): Promise<void> => {
        setIsEditing(true);
        setEditingAuthor({...author});
    }


    return (
        <div className={classes.authorsPageContainer}>
            <CreateAuthorForm onAuthorCreated={fetchAuthors}/>
            <section className={classes.tableContainer}>
                <Table
                    columns={authorsTableColumns}
                    dataSource={authorsStore.authors}
                    rowKey="id"
                />
            </section>
            <Modal
                title='Edit author'
                open={isEditing}
                onCancel={() => setIsEditing(false)}
                footer={null}
            >
                {editingAuthor && (
                    <UpdateAuthorForm
                        onAuthorUpdated={
                            async () => {
                                await fetchAuthors();
                                setIsEditing(false);
                            }}
                        editingAuthor={editingAuthor}
                    />
                )}
            </Modal>
        </div>
    );
}

export default AuthorsPage;