import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useAuth } from './AuthContext';
import './EventCard.css';

const EventCard = () => {
    const { eventId } = useParams();
    const [event, setEvent] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [formData, setFormData] = useState({
        tickets: 1,
        firstName: '',
        lastName: '',
        email: '',
    });

    const navigate = useNavigate();
    const { token, logout } = useAuth();

    useEffect(() => {
        const fetchEventInfo = async () => {
            try {
                const response = await fetch(`http://localhost:8080/api/event_card/show_info/${eventId}`, {
                    headers: { Authorization: `Bearer ${token}` },
                });

                if (response.status === 401) {
                    logout();
                }

                if (!response.ok) {
                    throw new Error('Ошибка при загрузке информации о событии');
                }

                const data = await response.json();
                setEvent({
                    ...data,
                    eventTime: new Date(data.eventTime).toLocaleString('ru-RU', {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit',
                    }),
                });
            } catch (error) {
                console.error('Ошибка загрузки информации о событии:', error.message);
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchEventInfo();
    }, [eventId, token, logout]);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleBooking = async () => {
        try {
            const response = await fetch(`http://localhost:8080/api/event_card/booking`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({
                    eventId,
                    firstName: formData.firstName,
                    lastName: formData.lastName,
                    email: formData.email,
                    tickets: formData.tickets,
                }),
            });

            if (response.status === 401) {
                logout();
                return;
            }

            if (!response.ok) {
                throw new Error('Ошибка бронирования билета');
            }

            const result = await response.json();
            alert(result.message || 'Бронирование прошло успешно!');
        } catch (error) {
            console.error('Ошибка бронирования:', error.message);
            alert('Не удалось забронировать билет.');
        }
    };

    if (loading) return <p className="loading-message">Загрузка информации о событии...</p>;
    if (error) return <p className="error-message">Ошибка: {error}</p>;

    return (
        <div className="event-card-page">
           <div className="header">
                <div className="header-left" onClick={() => navigate('/main_page')}>AfishTunchik</div>
                <div className="header-buttons">
                    <button onClick={() => navigate('/my_bookings')}>Я посещаю</button>
                    <button onClick={() => navigate('/my_events')}>Я организую</button>
                    <button onClick={() => navigate('/create_event')}>+Создать событие</button>
                    <button onClick={() => logout()}>Выйти</button>
                </div>
                </div>
            <div className="event-details">
                <h1>{event.title}</h1>
                <p className="event-description">{event.description}</p>
                <h3>{`${event.eventTime} | ${event.venue}`}</h3>
            </div>
            <div className="registration-form">
                <h2>Зарегистрироваться</h2>
                <form>
                    <label>
                        Количество билетов:
                        <input
                            type="number"
                            name="tickets"
                            min="1"
                            value={formData.tickets}
                            onChange={handleInputChange}
                        />
                    </label>
                    <label>
                        Имя:
                        <input
                            type="text"
                            name="firstName"
                            value={formData.firstName}
                            onChange={handleInputChange}
                        />
                    </label>
                    <label>
                        Фамилия:
                        <input
                            type="text"
                            name="lastName"
                            value={formData.lastName}
                            onChange={handleInputChange}
                        />
                    </label>
                    <label>
                        Почта:
                        <input
                            type="email"
                            name="email"
                            value={formData.email}
                            onChange={handleInputChange}
                        />
                    </label>
                    <button type="button" onClick={handleBooking}>
                        Зарегистрироваться
                    </button>
                </form>
            </div>
        </div>
    );
};

export default EventCard;
