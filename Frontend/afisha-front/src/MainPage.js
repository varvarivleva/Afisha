import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from './AuthContext';
import './MainPage.css';

const MainPage = () => {
    const [events, setEvents] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const { token, logout } = useAuth(); // Используем токен и функцию выхода

    useEffect(() => {
        const fetchEvents = async () => {
            try {
                const response = await fetch('http://localhost:8080/api/main_page/show_events', {
                    headers: {
                        Authorization: `Bearer ${token}`, // Добавляем заголовок авторизации
                    },
                });

                if (response.status === 401) {
                    logout(); // Выход при ошибке авторизации
                    return;
                }

                if (!response.ok) {
                    throw new Error('Ошибка при загрузке событий');
                }

                const data = await response.json();
                if (data.events && Array.isArray(data.events)) {
                    const formattedEvents = data.events.map((event) => ({
                        ...event,
                        eventTime: new Date(event.eventTime).toLocaleString('ru-RU', {
                            year: 'numeric',
                            month: 'long',
                            day: 'numeric',
                            hour: '2-digit',
                            minute: '2-digit',
                        }),
                    }));
                    setEvents(formattedEvents);
                } else {
                    throw new Error('Некорректный формат данных с сервера');
                }
            } catch (error) {
                console.error('Ошибка загрузки событий:', error.message);
                setError(error.message);
                setEvents([]);
            } finally {
                setLoading(false);
            }
        };

        fetchEvents();
    }, [token, logout]);

    const handleEventClick = (eventId) => {
        navigate(`/event_card/${eventId}`);
    };

    return (
        <div className="main-page">
            <div className="header">
                <div className="header-left" onClick={() => navigate('/main_page')}>AfishTunchik</div>
                <div className="header-buttons">
                    <button onClick={() => navigate('/my_bookings')}>Я посещаю</button>
                    <button onClick={() => navigate('/my_events')}>Я организую</button>
                    <button onClick={() => navigate('/create_event')}>+Создать событие</button>
                    <button onClick={() => logout()}>Выйти</button>
                </div>
                </div>
            <div className="main-content">
                <h1>ЖИЗНЬ ПРОДОЛЖАЕТСЯ ЗДЕСЬ! ПОСЕЩАЙТЕ СОБЫТИЯ И ЖИВИТЕ ЛУЧШУЮ ЖИЗНЬ ВМЕСТЕ С AfishTunchik</h1>
                <div className="events-list">
                    {loading && <p className="loading-message">Загрузка событий...</p>}
                    {error && <p className="error-message">Ошибка: {error}</p>}
                    {!loading && !error && events.length > 0 ? (
                        events.map((event) => (
                            <div
                                key={event.id}
                                className="event-card"
                                onClick={() => handleEventClick(event.id)}
                            >
                                <div className="event-date">{event.eventTime}</div>
                                <h2>{event.title}</h2>
                                <p>{event.description.substring(0, 70)}...</p>
                                <div className="event-venue">{event.venue}</div>
                            </div>
                        ))
                    ) : (
                        !loading &&
                        !error && (
                            <p className="no-events">Событий пока нет. Будьте первым, кто создат его!</p>
                        )
                    )}
                </div>
            </div>
        </div>
    );
};

export default MainPage;
