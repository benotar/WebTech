import {FC} from 'react';
import {useAuthStore} from "../../stores/useAuthStore.ts";
import {Button, DatePicker, Form, Input, message, Typography} from 'antd';
import classes from './RegisterPage.module.css';
import {useNavigate} from "react-router-dom";

const RegisterPage: FC = () => {

    const [form] = Form.useForm();

    const {register} = useAuthStore();
    const navigate = useNavigate();


    const handleLogin = async () => {
        try{

            const values = await form.validateFields();

            await register(values);

            console.log('Received values from form: ', values);

            message.success('Registration successful!');

            navigate('/');
        }catch(error){

            console.log('Failed register: ', error);

            message.error('Failed register!' );
        }
    }

    return (
        <div className={classes.appBg}>
            <Form
                form={form}
                className={classes.loginForm}
            >
                <Typography.Title>Welcome!</Typography.Title>

                <Form.Item
                    label='Username'
                    name='userName'
                    rules={[{ required: true, message: 'Please enter username' }]}
                >
                    <Input placeholder='Enter username' />
                </Form.Item>

                <Form.Item
                    label='Password'
                    name='password'
                    rules={[{ required: true, message: 'Please enter password' }]}
                >
                    <Input.Password placeholder='Enter password' />
                </Form.Item>

                <Form.Item
                    label='First Name'
                    name='firstName'
                    rules={[{ required: true, message: 'Please enter your first name' }]}
                >
                    <Input placeholder='Enter your first name' />
                </Form.Item>

                <Form.Item
                    label='Last Name'
                    name='lastName'
                    rules={[{ required: true, message: 'Please enter your last name' }]}
                >
                    <Input placeholder='Enter your last name' />
                </Form.Item>

                <Form.Item
                    label="Birth Date"
                    name="birthDate"
                    rules={[
                        { required: true, message: "Please select birth date" },
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

                        className={classes.datePicker}
                    />
                </Form.Item>

                <Form.Item
                    label='Address'
                    name='address'
                    rules={[{ required: true, message: 'Please enter your address' }]}
                >
                    <Input placeholder='Enter your address' />
                </Form.Item>

                <Form.Item>
                    <Button type='primary' htmlType='submit' block onClick={handleLogin}>
                        Register
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default RegisterPage;