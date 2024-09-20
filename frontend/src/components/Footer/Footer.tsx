// src/components/Footer.js

import React from 'react';
import './Footer.css'; 

const Footer = () => {
  return (
    <footer className="footer">
      <div className="footer-container">
        <div className="footer-section">
          <h3>О нас</h3>
          <p>
            Мы предоставляем качественные услуги в различных областях. 
            Наша цель — обеспечить высокий уровень сервиса и удовлетворение потребностей наших клиентов.
          </p>
        </div>
        <div className="footer-section">
          <h3>Навигация</h3>
          <ul>
            <li><a href="/">Главная</a></li>
            <li><a href="/catalog">Каталог</a></li>
          </ul>
        </div>
        <div className="footer-section">
          <h3>Контакты</h3>
          <p>Телефон: +1 234 567 890</p>
          <p>Email: contact@example.com</p>
          <p>Адрес: ул. Примерная, д. 1, Город, Страна</p>
        </div>
        <div className="footer-section">
          <h3>Социальные сети</h3>
          <ul>
          <li><a href="https://facebook.com" target="_blank" rel="noopener noreferrer">Facebook</a></li>
          <li><a href="https://twitter.com" target="_blank" rel="noopener noreferrer">Twitter</a></li>
          <li><a href="https://instagram.com" target="_blank" rel="noopener noreferrer">Instagram</a></li>
          </ul>
        </div>
      </div>
      <div className="footer-bottom">
        <p>&copy; {new Date().getFullYear()} Ваш сайт. Все права защищены.</p>
        <p>
          <a href="/privacy-policy">Политика конфиденциальности</a> | 
          <a href="/terms-of-service"> Условия использования</a>
        </p>
      </div>
    </footer>
  );
};

export default Footer;
