import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import EnterPageRegister from './EnterPage';
import Authorization from './Authorizaton.js';

function App() {
    return (
        <Router>
            <div>
                <h1>Event Booking Service</h1>
                <Routes>
                    <Route path="/" element={<EnterPageRegister />} />
                    <Route path="/authorization" element={<Authorization />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
