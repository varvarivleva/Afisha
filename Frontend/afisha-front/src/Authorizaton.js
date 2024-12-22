import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Authorization.css'; // Подключаем CSS для стилизации

const Authorization = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState(''); // Состояние для хранения сообщения об ошибке
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErrorMessage(''); // Сбрасываем сообщение об ошибке перед новым запросом

        try {
            const response = await fetch('http://localhost:8080/api/enter_page/authorization', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ username, password }),
            });

            const responseData = await response.json(); // Получаем тело ответа

            // Обработка ошибок 400
            if (response.status === 400 && responseData.message === 'All fields are required') {
                throw new Error('400: Не все поля заполнены');
            }

            if (response.status === 400 && responseData.message === 'Invalid username') {
                throw new Error('400: Такого пользователя не существует');
            }

            if (response.status === 400 && responseData.message === 'Invalid password') {
                throw new Error('400: Неверный пароль');
            }

            // Обработка ошибки 500
            if (response.status === 500) {
                throw new Error('500: Ошибка сервера, повторите попытку позже');
            }

            if (!response.ok) {
                throw new Error(responseData.message || 'Ошибка сервера');
            }

            console.log('Успешная авторизация:', responseData);
            navigate('/main_page');
        } catch (error) {
            console.error('Ошибка авторизации:', error.message);
            setErrorMessage(error.message); // Устанавливаем сообщение об ошибке
        }
    };

    return (
        <div className="auth-container">
            {errorMessage && ( // Показываем сообщение об ошибке
                <div className="error-popup">
                    {errorMessage}
                </div>
            )}
            <div className="auth-card">
                <h2>Авторизация</h2> {/* Заголовок страницы */}
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
