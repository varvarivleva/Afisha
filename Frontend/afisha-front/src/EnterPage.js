import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom'; // Импортируем useNavigate вместо useHistory

const EnterPageRegister = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const navigate = useNavigate(); // Используем useNavigate

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('http://localhost:7005/api/enter_page/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ username, password, email }),
            });

            if (response.status === 400) { // Исправлено на "==="
                const errorData = await response.json();
                throw new Error('Должны быть заполнены все поля');
            }

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Ошибка сервера');
            }

            const data = await response.json();
            console.log(data);

        } catch (error) {
            console.error('Error during fetch:', error);
        }
    };

    // Функция для перехода на страницу авторизации
    const handleSignIn = () => {
        navigate('/authorization'); // Используем navigate для перехода
    };

    return (
        <form onSubmit={handleSubmit}>
            <input type="text" placeholder="Username" onChange={(e) => setUsername(e.target.value)} />
            <input type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)} />
            <input type="email" placeholder="Email" onChange={(e) => setEmail(e.target.value)} />
            <button type="submit">Register</button>
            <button type="button" onClick={handleSignIn}>Sign in</button>
        </form>
    );
};

export default EnterPageRegister;
