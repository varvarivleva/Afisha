import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './MainPage.css';

const MainPage = () => {
    const [events, setEvents] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchEvents = async () => {
            try {
                const response = await fetch('http://localhost:8080/api/main_page/show_events');
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
    }, []);

    const handleEventClick = (eventId) => {
        navigate(`/event/${eventId}`);
    };

    return (
        <div className="main-page">
            <div className="header">
                <button onClick={() => navigate('/my_bookings')}>Я посещаю</button>
                <button onClick={() => navigate('/my_events')}>Я организую</button>
                <button onClick={() => navigate('/create_event')}>+Создать событие</button>
                <button onClick={() => navigate('/account')}>Акк</button>
            </div>
            <div className="main-content">
                <h1>ХАЙП НАЧИНАЕТСЯ ЗДЕСЬ! РЕГИСТРИРУЙТЕСЬ НА СОБЫТИЯ И БУДЬТЕ ХАЙПОРОЖАМИ</h1>
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
                            <p className="no-events">������� ���� ���. ������ ������, ��� ������� ���!</p>
                        )
                    )}
                </div>
            </div>
        </div>
    );
};

export default MainPage;
