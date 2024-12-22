import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './EnterPage.css';

const EnterPageRegister = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState(''); // Добавляем состояние для email
    const [errorMessage, setErrorMessage] = useState(''); // Добавляем состояние для ошибок
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErrorMessage(''); // Сбрасываем сообщение об ошибке перед новым запросом

        try {
            const response = await fetch('http://localhost:8080/api/enter_page/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ username, password, email }),
            });

            if (response.status === 400 & response.message === 'All fields are required') { // Обрабатываем 400-ую ошибку
                throw new Error('400: Не все поля заполнены');
            }

            if (response.status === 400 & response.message === 'User already exists') { // Обрабатываем 400-ую ошибку
                throw new Error('400: Такой пользователь уже существует');
            }

            if (response.status === 500) { // Обрабатываем 500-ую ошибку
                throw new Error('500: Ошибка сервера, повторите попытку позже');
            }

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Ошибка сервера');
            }

            const data = await response.json();
            console.log('Registration successful:', data);
            navigate('/main_page');
        } catch (error) {
            console.error('Error during fetch:', error.message);
            setErrorMessage(error.message); // Устанавливаем сообщение об ошибке
        }
    };

    const handleSignIn = () => navigate('/authorization');

    return (
        <div className="register-container">
            {errorMessage && ( // Показываем сообщение об ошибке
                <div className="error-popup">
                    {errorMessage}
                </div>
            )}
            <div className="register-card">
                <h2>Регистрация</h2>
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
                    <input
                        type="email"
                        placeholder="E-mail" // Добавляем поле email
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    <button type="submit" className="register-button" onClick={handleSignIn}>
                        Зарегистрироваться
                    </button>
                </form>
                <button className="signin-link" onClick={handleSignIn}>
                    Уже зарегистрирован
                </button>
            </div>
        </div>
    );
};

export default EnterPageRegister;
