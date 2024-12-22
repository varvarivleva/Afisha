import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from './AuthContext';
import './CreateEvent.css';

const CreateEvent = () => {
    const { token, logout } = useAuth();
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        title: '',
        description: '',
        eventTime: '',
        venue: '',
        ticketsAvailable: '',
        ticketPrice: '',
    });
    const [error, setError] = useState(null);
    const [successMessage, setSuccessMessage] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);
        setSuccessMessage('');

        try {
            const response = await fetch('http://localhost:8080/api/event_card/create_event', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({
                    title: formData.title,
                    description: formData.description,
                    eventTime: new Date(formData.eventTime).toISOString(),
                    venue: formData.venue,
                    ticketsAvailable: parseInt(formData.ticketsAvailable, 10),
                    ticketPrice: parseFloat(formData.ticketPrice),
                }),
            });

            if (response.status === 401) {
                logout();
            }

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Ошибка при создании события');
            }

            setSuccessMessage('Событие успешно создано!');
            setFormData({
                title: '',
                description: '',
                eventTime: '',
                venue: '',
                ticketsAvailable: '',
                ticketPrice: '',
            });
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div className="create-event-page">
            <div className="header">
                <div className="header-left" onClick={() => navigate('/main_page')}>ashay tunchik</div>
                <div className="header-buttons">
                    <button onClick={() => navigate('/my_bookings')}>Я посещаю</button>
                    <button onClick={() => navigate('/my_events')}>Я организую</button>
                    <button onClick={() => navigate('/create_event')}>+Создать событие</button>
                    <button onClick={() => logout()}>Выйти</button>
                </div>
            </div>
            <div className="main-content">
                <h1>Создание события</h1>
                {error && <p className="error-message">{error}</p>}
                {successMessage && <p className="success-message">{successMessage}</p>}
                <form className="event-form" onSubmit={handleSubmit}>
                    <input
                        type="text"
                        name="title"
                        value={formData.title}
                        placeholder="Название"
                        onChange={handleChange}
                        required
                    />
                    <textarea
                        name="description"
                        value={formData.description}
                        placeholder="Описание"
                        onChange={handleChange}
                        required
                    />
                    <input
                        type="datetime-local"
                        name="eventTime"
                        value={formData.eventTime}
                        onChange={handleChange}
                        required
                    />
                    <input
                        type="text"
                        name="venue"
                        value={formData.venue}
                        placeholder="Место проведения"
                        onChange={handleChange}
                        required
                    />
                    <input
                        type="number"
                        name="ticketsAvailable"
                        value={formData.ticketsAvailable}
                        placeholder="Количество билетов"
                        onChange={handleChange}
                        required
                    />
                    <input
                        type="number"
                        step="0.01"
                        name="ticketPrice"
                        value={formData.ticketPrice}
                        placeholder="Цена билета"
                        onChange={handleChange}
                        required
                    />
                    <button type="submit">Опубликовать</button>
                </form>
            </div>
        </div>
    );
};

export default CreateEvent;
