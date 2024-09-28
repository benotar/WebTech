import {FC} from "react";
import classes from './CreateBookForm.module.css';
import {Button, Form, Input, message, Typography} from "antd";
import {useBooksStore} from "../../stores/useBooksStore.ts";

interface ICreateBookFormProps {
    onBookCreated: () => Promise<void>;
}


const CreateBookForm: FC<ICreateBookFormProps> = ({onBookCreated}) => {

    const [form] = Form.useForm();

    const bookStore = useBooksStore();

    const onCreateBook = async () => {

        try {
            const values = await form.validateFields();

            await bookStore.createBook(values);
            form.resetFields();
            await onBookCreated();
        } catch (error) {
            console.log('Form validation failed: ', error);
            message.error('Invalid input data!');
        }
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
                        required: true, message: 'Please enter genre'
                    },
                    {
                        min: 3, message: 'Genre must be at least 3 characters'
                    }
                ]}
            >
                <Input placeholder='Enter genre'/>
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
                        transform: (value) => Number(value),
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
                label='Author last name'
                name='authorLastName'
                rules={[
                    {required: true, message: "Please enter author's last name"},
                    {min: 2, message: "Author's last name must be at least 2 characters"},
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