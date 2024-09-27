import {FC, useState} from 'react';
import {useAuthStore} from "../../store/store.ts";

const LoginForm: FC = () => {

    const {isAuthenticated, login, errorCode} =  useAuthStore();

    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [fingerprint, setFingerprint] = useState('');

    const hanldeLogin = async () => {
        await login({
           userName: userName,
           password: password,
           fingerprint: fingerprint
        });
    }

    return (
        <div>
            <input
                onChange={e => setUserName(e.target.value)}
                value={userName}
                type="text"
                placeholder="Username"
            />
            <input
                onChange={e => setPassword(e.target.value)}
                value={password}
                type="text"
                placeholder="Password"
            />
            <input
                onChange={e => setFingerprint(e.target.value)}
                value={fingerprint}
                type="text"
                placeholder="Fingerprint"
            />

            <button onClick={hanldeLogin}>Login</button>

            <h3>Is Authenticated: {isAuthenticated ? 'Yes' : 'No'}</h3>
            <h3>Error code: {errorCode ? errorCode : 'No'}</h3>
        </div>
    );
};

export default LoginForm;