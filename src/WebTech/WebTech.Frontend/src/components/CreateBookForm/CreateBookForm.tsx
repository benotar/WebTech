import {FC, useState} from "react";
import classes from "../LoginForm/LoginForm.module.css";
import {Button, Form, Input, Typography} from "antd";
import {useBooksStore} from "../../stores/useBooksStore.ts";

interface ICreateBookFormProps {
    onBookCreated: () => void;
}


const CreateBookForm: FC<ICreateBookFormProps> = ({onBookCreated}) => {

    const [form] = Form.useForm();

    const [title, setTitle] = useState('');
    const [genre, setGenre] = useState('');
    const [publicationYear, setPublicationYear] = useState(-1);
    const [authorFirstName, setAuthorFirstName] = useState('');
    const [authorLastName, setAuthorLastName] = useState('');

    const bookStore = useBooksStore();

    const onCreateBook = async () => {

        try {
            const values = await form.validateFields();

            // await bookStore.createBook({
            //     title: values.title,
            //     genre: values.genre,
            //     publicationYear: values.publicationYear,
            //     authorFirstName: values.authorFirstName,
            //     authorLastName: values.authorLastName
            // });

            await bookStore.createBook(values);
            form.resetFields();
            onBookCreated();
        } catch (error) {
            console.log('Form validation failed: ', error);
        }


        // await bookStore.createBook({
        //     title: title,
        //     genre: genre,
        //     publicationYear: publicationYear,
        //     authorFirstName: authorFirstName,
        //     authorLastName: authorLastName
        // });

        form.resetFields();

        onBookCreated();
    }

    return (
        <Form form={form} className={classes.createForm}>

            <Typography.Title>Welcome to books page!</Typography.Title>

            <Form.Item
                label='Title'
                name='title'
                rules={[
                    {
                        required: true, message: 'Please enter title'
                    },
                    {
                        min: 3, message: 'Title must be at least 3 characters'
                    }
                ]}
            >
                <Input placeholder='Enter title'/>
            </Form.Item>

            <Form.Item
                label='Genre'
                name='genre'
                rules={[
                    {
                        required: true, message: 'Please enter title'
                    },
                    {
                        min: 3, message: 'Genre must be at least 3 characters'
                    }
                ]}
            >
                <Input placeholder='genre'/>
            </Form.Item>
            <Form.Item
                label='Publication year'
                name='publicationYear'
                rules={[
                    {required: true, message: "Please enter publication year"},
                    {
                        type: "number",
                        min: 1800,
                        max: new Date().getFullYear(),
                        transform: (value) => Number(value), // Перетворюємо в число
                        message: `Year must be between 1800 and ${new Date().getFullYear()}`,
                    },
                ]}
            >
                <Input placeholder='Enter publication year'/>
            </Form.Item>
            <Form.Item
                label='Author first name'
                name='authorFirstName'
                rules={[
                    {required: true, message: "Please enter author's first name"},
                    {min: 2, message: "Author's first name must be at least 2 characters"},
                ]}
            >
                <Input placeholder='Enter author first name'/>
            </Form.Item>
            <Form.Item
                label='Author first name'
                name='authorLastName'
                rules={[
                    { required: true, message: "Please enter author's last name" },
                    { min: 2, message: "Author's last name must be at least 2 characters" },
                ]}
            >
                <Input placeholder='Enter author last name'/>
            </Form.Item>
            <Button type='primary' htmlType='submit' onClick={onCreateBook}>
                Add a new book
            </Button>
        </Form>
    );
}

export default CreateBookForm;