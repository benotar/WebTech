import {FC} from "react";
import {Button, DatePicker, Form, Input, message, Typography} from "antd";
import {useAuthorsStore} from "../../stores/useAuthorsStore.ts";
import classes from './CreateAuthorForm.module.css';

interface ICreateAuthorFormProps {
    onAuthorCreated: () => Promise<void>;
}

const CreateAuthorForm: FC<ICreateAuthorFormProps> = ({onAuthorCreated}) => {
    const [form] = Form.useForm();
    const authorStore = useAuthorsStore();

    const onCreateAuthor = async () => {

        try {
            const values = await form.validateFields();

            await authorStore.createAuthor(values);
            form.resetFields();
            await onAuthorCreated();
        } catch (error) {
            console.log('Form validation failed: ', error);
            message.error('Invalid input data!');
        }
    }

    return (
        <Form form={form} className={classes.createForm}>

            <Typography.Title>Welcome to authors page!</Typography.Title>
            <Form.Item
                label='First name'
                name='firstName'
                rules={[
                    {required: true, message: "Please enter first name"},
                    {min: 2, message: "First name must be at least 2 characters"},
                ]}
            >
                <Input placeholder='Enter first name'/>
            </Form.Item>
            <Form.Item
                label='Last name'
                name='lastName'
                rules={[
                    {required: true, message: "Please enter last name"},
                    {min: 2, message: "Last name must be at least 2 characters"},
                ]}
            >
                <Input placeholder='Enter last name'/>
            </Form.Item>
            <Form.Item
                label="Birth Date"
                name="birthDate"
                rules={[
                    {required: true, message: "Please select birth date"},
                    {
                        validator: (_, value) => {
                            if (!value) {
                                return Promise.reject(new Error('Please select a valid birth date'));
                            }

                            const today = new Date();
                            // Зрівнюємо дати до початку дня, інакше можуть бути проблеми з часовими зонами
                            const selectedDate = new Date(value);
                            selectedDate.setHours(0, 0, 0, 0);
                            today.setHours(0, 0, 0, 0);

                            if (selectedDate > today) {
                                return Promise.reject(new Error('Birth date cannot be in the future'));
                            }

                            return Promise.resolve();
                        },
                    },
                ]}
            >
                <DatePicker
                    placeholder="Select birth date"
                    style={{ width: '100%' }}
                />
            </Form.Item>
            <Button type='primary' htmlType='submit' onClick={onCreateAuthor}>
                Add a new author
            </Button>
        </Form>
    );
}

export default CreateAuthorForm;