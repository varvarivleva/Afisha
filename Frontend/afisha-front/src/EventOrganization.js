import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from './AuthContext';
import './EventOrganization.css';

const EventOrganization = () => {
    const [organizedEvents, setOrganizedEvents] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const { token, logout } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        const fetchOrganizedEvents = async () => {
            try {
                const response = await fetch('http://localhost:8080/api/my_events/organization', {
                    headers: { Authorization: `Bearer ${token}` },
                });

                if (response.status === 401) {
                    logout(); // Если токен недействителен, разлогиниваем пользователя
                }

                if (!response.ok) {
                    throw new Error('Ошибка при загрузке мероприятий');
                }

                const data = await response.json();
                setOrganizedEvents(
                    data.map((event) => ({
                        ...event,
                        eventTime: new Date(event.eventTime).toLocaleString('ru-RU'),
                    }))
                );
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchOrganizedEvents();
    }, [token, logout]);

    return (
        <div className="event-organization-page">
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
                <h1>Я организовываю</h1>
                {loading && <p>Загрузка...</p>}
                {error && <p>Ошибка: {error}</p>}
                {!loading && organizedEvents.length === 0 && <p>Нет мероприятий</p>}
                {!loading && organizedEvents.length > 0 && (
                    <table className="events-table">
                        <thead>
                            <tr>
                                <th>Название</th>
                                <th>Дата и время</th>
                                <th>Количество билетов</th>
                                <th>Ссылка на событие</th>
                            </tr>
                        </thead>
                        <tbody>
                            {organizedEvents.map((event) => (
                                <tr key={event.id}>
                                    <td>{event.title}</td>
                                    <td>{event.eventTime}</td>
                                    <td>{event.ticketsAvailable}</td>
                                    <td>
                                        <button
                                            onClick={() => navigate(`/event_card/${event.id}`)}
                                            className="event-link"
                                        >
                                            тык
                                        </button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                )}
            </div>
        </div>
    );
};

export default EventOrganization;
