import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import EnterPageRegister from './EnterPage';
import Authorization from './Authorizaton.js';
import MainPage from './MainPage.js';
import './App.css';
import './EnterPage.css'; // Импорт стилей

function App() {
    return (
        <Router>
            <div>
                <Routes>
                    <Route path="/" element={<EnterPageRegister />} />
                    <Route path="/authorization" element={<Authorization />} />
                    <Route path="/main_page" element={<MainPage />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
