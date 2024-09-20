import React, { useEffect } from 'react';
import { useAssistanceStore } from '../../store/useAssistanceStore.ts';
import { Link } from 'react-router-dom';
import styles from './Home.module.css';

const Home = () => {
  const assistances = useAssistanceStore((state) => state.assistances);
  const fetchAssistances = useAssistanceStore((state) => state.fetchAssistances);
  const pageNumber = useAssistanceStore((state) => state.pageNumber);
  const pageSize = useAssistanceStore((state) => state.pageSize);

  useEffect(() => {
    fetchAssistances(pageNumber, pageSize);
  }, [fetchAssistances, pageNumber, pageSize]);

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <h1>Объявления</h1>
        <Link to="/create-assistance" className={styles.createButton}>
          Создать Объявление
        </Link>
      </div>
      <div className={styles.assistanceList}>
        {assistances.length === 0 ? (
          <p>Нет доступных объявлений.</p>
        ) : (
          assistances.map((assistance) => (
            <div key={assistance.id} className={styles.assistanceCard}>
              <h2>{assistance.title}</h2>
              <p>{assistance.description}</p>
              <p>Цена: {assistance.price}</p>
              <p>Длительность: {assistance.durationInMinutes} минут</p>
              <p>Местоположение: {assistance.location}</p>
              <Link to={`/assistances/${assistance.id}`} className={styles.detailLink}>Подробнее</Link>
            </div>
          ))
        )}
      </div>
    </div>
  );
};

export default Home;
