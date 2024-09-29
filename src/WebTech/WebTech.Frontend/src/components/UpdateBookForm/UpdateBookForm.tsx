import {FC, useEffect} from "react";
import {Button, Form, Input, message} from "antd";
import IBook from "../../interfaces/entities/IBook.ts";
import {useBooksStore} from "../../stores/useBooksStore.ts";

interface IUpdateBookFormProps {
    onBookUpdated: () => Promise<void>;
    editingBook: IBook;
}


const UpdateBookForm: FC<IUpdateBookFormProps> = ({onBookUpdated, editingBook}) => {

    const [form] = Form.useForm();
    const bookStore = useBooksStore();

    useEffect(() => {
        if (editingBook) {
            form.setFieldsValue({
                title: editingBook.title,
                genre: editingBook.genre,
                publicationYear: editingBook.publicationYear
            });
        }
    }, [editingBook, form]);

    const onUpdateBook = async () => {

        try {
            const values = await form.validateFields();

            await bookStore.updateBook({
                bookId: editingBook.id,
                title: values.title,
                genre: values.genre,
                publicationYear: values.publicationYear,
            });


            form.resetFields();

            await onBookUpdated();

            message.success('Book record updated successfully!');
        } catch (error) {
            console.log('Form validation failed: ', error);

            message.error('Invalid input data!');
        }
    }

    return (
        <Form
            form={form}
            initialValues={editingBook || []}
        >

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
                <Input/>
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
                <Input/>
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
                <Input/>
            </Form.Item>
            <Form.Item>
                <Form.Item>
                    <Button type="primary" onClick={onUpdateBook}>Save</Button>
                </Form.Item>
            </Form.Item>
        </Form>
    );
}

export default UpdateBookForm;