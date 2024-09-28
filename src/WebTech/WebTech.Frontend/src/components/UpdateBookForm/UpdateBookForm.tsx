import {FC} from "react";
import {Button, Form, Input} from "antd";
import IBook from "../../interfaces/entities/IBook.ts";
import {useBooksStore} from "../../stores/useBooksStore.ts";

interface IUpdateBookFormProps {
    onBookCreated: () => Promise<void>;
    editingBook: IBook;
    // onUpdate: (updatedValues: IBook) => void;
}




const UpdateBookForm: FC<IUpdateBookFormProps> = ({onBookCreated, editingBook}) => {

    const [form] = Form.useForm();
    const bookStore = useBooksStore();


    const onUpdateBook = async () => {

        try {
            const values = await form.validateFields();


            await bookStore.updateBook({
                bookId: editingBook.id,
                title: values.title,
                genre: values.genre,
                publicationYear: values.publicationYear,
                authorFirstName: editingBook.
            });

            form.resetFields();
            await onBookCreated();
        } catch (error) {
            console.log('Form validation failed: ', error);
        }
    }

    return(
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