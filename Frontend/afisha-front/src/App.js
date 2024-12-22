import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import EnterPageRegister from './EnterPage.js';
import Authorization from './Authorizaton.js';
import MainPage from './MainPage.js';
import EventCard from './EventCard.js';
import MyEvent from './EventOrganization.js';
import { AuthProvider, useAuth } from './AuthContext.js';
import './App.css';

const ProtectedRoute = ({ children }) => {
    const { token } = useAuth();
    return token ? children : <Navigate to="/authorization" />;
};

function App() {
    return (
        <Router>
            <AuthProvider>
                <div>
                    <Routes>
                        <Route path="/" element={<EnterPageRegister />} />
                        <Route path="/authorization" element={<Authorization />} />
                        <Route
                            path="/main_page"
                            element={<ProtectedRoute><MainPage /></ProtectedRoute>}
                        />
                        <Route
                            path="/event_card/:eventId"
                            element={<ProtectedRoute><EventCard /></ProtectedRoute>}
                        />
                        <Route
                            path="/my_events"
                            element={<ProtectedRoute><MyEvent /></ProtectedRoute>}
                        />
                    </Routes>
                </div>
            </AuthProvider>
        </Router>
    );
}

export default App;
