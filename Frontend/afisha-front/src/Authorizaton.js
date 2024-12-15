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
        body: JSON.stringify({ username, password }),
    });

    if (!response.ok) {
        console.error('Response error:', response.status);
        const errorData = await response.json();
        throw new Error(errorData.message || 'Server error');
    }

    const data = await response.json();
    console.log('Success:', data);
} catch (error) {
    console.error('Network or server error:', error.message);
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
