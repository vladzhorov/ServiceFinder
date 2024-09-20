
import React from 'react';
import './AdBlock.css'; // Добавьте стили для рекламы

const AdBlock = () => {
  return (
    <div className="ad-block">
      <h3>Реклама</h3>
      <div className="ad-card">
        <h4>Специальное предложение!</h4>
        <p>Получите 20% скидку на все услуги до конца месяца.</p>
      </div>
      <div className="ad-card">
        <h4>Профессиональные услуги</h4>
        <p>Мы предлагаем широкий спектр услуг по ремонту и обслуживанию.</p>
      </div>
    </div>
  );
};

export default AdBlock;
