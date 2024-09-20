import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { useAxiosInterceptors } from './shared/helpers/client.ts';
import Home from './pages/Home/Home.tsx';
import Catalog from './pages/Catalog/Catalog.tsx';
import Profile from './pages/Profile/Profile.tsx';
import Header from './components/Header/Header.tsx';
import Footer from './components/Footer/Footer.tsx';
import CreateAssistance from './components/CreateAssistance/CreateAssistance.tsx';
import AssistanceDetail from './components/AssistanceDetails/AssistanceDetails.tsx';
import Register from './components/Register/Register.tsx'; 
import Login from './components/Login/Login.tsx'; 
import './App.css';

const App = () => {
  useAxiosInterceptors(); // Подключаем перехватчик запросов

  return (
    <BrowserRouter>
      <Header />
      <div className="content">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/catalog" element={<Catalog />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/create-assistance" element={<CreateAssistance />} />
          <Route path="/assistances/:id" element={<AssistanceDetail />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} /> {/* Добавьте маршрут для страницы регистрации */}
        </Routes>
      </div>
      <Footer />
    </BrowserRouter>
  );
};

export default App;
