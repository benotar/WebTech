import {FC, useEffect, useState} from 'react';
import {useAuthStore} from "../../stores/useAuthStore.ts";
import {v4 as uuidv4} from 'uuid';
import {Button, Form, Input, message, Typography} from 'antd';
import classes from './LoginPage.module.css';
import {useNavigate} from "react-router-dom";

const LoginPage: FC = () => {


    const {login} = useAuthStore();
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [fingerprint, setFingerprint] = useState('');
    const navigate = useNavigate();


    useEffect(() => {
        const generatedFingerprint = uuidv4();

        setFingerprint(generatedFingerprint);
    }, []);

    const handleLogin = async () => {

        try{
            await login({
                userName: userName,
                password: password,
                fingerprint: fingerprint
            });

            navigate('/');
        }catch (error) {
            console.log('Error login: ', error);
            message.error('Incorrect username or password. Please try again.');
        }
    }

    return (
        <div className={classes.appBg}>

            <Form className={classes.loginForm}>

                <Typography.Title>Welcome Back!</Typography.Title>

                <Form.Item
                    rules={[
                        {
                            required: true,
                            type: 'string',
                            message: 'Please enter username'
                        }
                    ]}
                    label='Username' name='myUsername'>

                    <Input onChange={e => setUserName(e.target.value)}
                           value={userName}
                           placeholder='Enter username'/>

                </Form.Item>

                <Form.Item
                    rules={[
                        {
                            required: true,
                            type: 'string',
                            message: 'Please enter password'
                        }
                    ]}
                    label='Password' name='myPassword'>

                    <Input.Password onChange={e => setPassword(e.target.value)}
                                    value={password}
                                    placeholder='Enter password'/>

                </Form.Item>

                <Button type='primary' htmlType='submit' block
                        onClick={handleLogin}>Login</Button>

            </Form>
        </div>
    );
};

export default LoginPage;