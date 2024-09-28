import IAuthor from "../../interfaces/entities/IAuthor.ts";
import {FC, useEffect} from "react";
import {Button, Form, Input, message} from "antd";
import {useAuthorsStore} from "../../stores/useAuthorsStore.ts";

interface IUpdateAuthorFormProps {
    onAuthorUpdated: () => Promise<void>;
    editingAuthor: IAuthor;
}

const UpdateAuthorForm: FC<IUpdateAuthorFormProps> = ({onAuthorUpdated, editingAuthor}) => {
    const [form] = Form.useForm();
    const authorStore = useAuthorsStore();

    useEffect(() => {
        if (editingAuthor) {
            form.setFieldsValue({
                firstName: editingAuthor.firstName,
                lastName: editingAuthor.lastName,
                birthDate: editingAuthor.birthDate
            });
        }
    }, [editingAuthor, form]);

    const onUpdateAuthor = async () => {

        try {
            const values = await form.validateFields();

            await authorStore.updateAuthor({
                authorId: editingAuthor.id,
                firstName: values.firstName,
                lastName: values.lastName,
                birthDate: values.birthDate
            });


            form.resetFields();
            await onAuthorUpdated();
            message.success('Author updated successfully!');
        } catch (error) {
            console.log('Form validation failed: ', error);
            message.error('Invalid input data!');
        }
    }

    return (
        <Form
            form={form}
            initialValues={editingAuthor || []}
        >

            <Form.Item
                label='First name'
                name='firstName'
                rules={[
                    {required: true, message: "Please enter first name"},
                    {min: 2, message: "First name must be at least 2 characters"},
                ]}
            >
                <Input/>
            </Form.Item>
            <Form.Item
                label='Last name'
                name='lastName'
                rules={[
                    {required: true, message: "Please enter last name"},
                    {min: 2, message: "Last name must be at least 2 characters"},
                ]}
            >
                <Input/>
            </Form.Item>
            <Form.Item
                label="Birth Date"
                name="birthDate"
                rules={[{ required: true,
                    type: "date",
                    message: "Please select birth date" }]}
            >
                {/*<DatePicker*/}
                {/*    placeholder="Select birth date"*/}
                {/*    style={{ width: '100%' }}*/}
                {/*/>*/}
                <Input/>

            </Form.Item>
            <Form.Item>
                <Form.Item>
                    <Button type="primary" onClick={onUpdateAuthor}>Save</Button>
                </Form.Item>
            </Form.Item>
        </Form>
    );
}

export default UpdateAuthorForm;