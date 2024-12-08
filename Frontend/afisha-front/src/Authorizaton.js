import React, { useState } from 'react';

const Authorization = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('http://localhost:7005/api/enter_page/authorization', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ username, password }), // Убрала email
            });

            if (response.status === 400) { // Исправлено
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

    return (
        <form onSubmit={handleSubmit}>
            <input type="text" placeholder="Username" onChange={(e) => setUsername(e.target.value)} />
            <input type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)} />
            <button type="submit">Authorization</button>
        </form>
    );
};

export default Authorization;
