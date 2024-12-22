import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from './AuthContext';
import './Authorization.css';

const Authorization = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const navigate = useNavigate();
    const { login } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErrorMessage('');

        try {
            const response = await fetch('http://localhost:8080/api/enter_page/authorization', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ username, password }),
            });

            const responseData = await response.json();

            if (!response.ok) {
                throw new Error(responseData.message || 'Ошибка авторизации');
            }

            login(responseData.token); // Сохраняем токен в контекст
            navigate('/main_page');
        } catch (error) {
            setErrorMessage(error.message);
        }
    };

    return (
        <div className="auth-container">
            {errorMessage && <div className="error-popup">{errorMessage}</div>}
            <div className="auth-card">
                <h2>Авторизация</h2>
                <form onSubmit={handleSubmit}>
                    <input
                        type="text"
                        placeholder="Логин"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                    />
                    <input
                        type="password"
                        placeholder="Пароль"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    <button type="submit" className="auth-button">
                        Войти
                    </button>
                </form>
            </div>
        </div>
    );
};

export default Authorization;
