import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL || 'https://localhost:7091/api';
const ORDER_API_URL = process.env.REACT_APP_ORDER_API_URL || 'http://localhost:5001/api';

export const client = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const orderClient = axios.create({
  baseURL: ORDER_API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const useAxiosInterceptors = () => {
  axios.interceptors.request.use(
    config => {
      const token = localStorage.getItem('authToken');
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    error => Promise.reject(error)
  );

  axios.interceptors.response.use(
    response => response,
    error => {
      // Обработка ошибок, например, если токен истек
      if (error.response && error.response.status === 401) {
        // Логика для обработки неавторизованного запроса
      }
      return Promise.reject(error);
    }
  );
};
