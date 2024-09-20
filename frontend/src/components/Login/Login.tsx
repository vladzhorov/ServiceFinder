import React, { useState } from 'react';
import { TextField, Button, Typography, Container, Box } from '@mui/material';
import axios from 'axios';

const Login = () => {
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [error, setError] = useState<string>('');
  const [loading, setLoading] = useState<boolean>(false);

  const handleLogin = async () => {
    setLoading(true);
    setError('');

    try {
      const response = await axios.post('https://localhost:7292/api/auth/login', {
        email,
        password,
      });

      const { token, userId } = response.data; // Предполагается, что API возвращает token и userId

      localStorage.setItem('authToken', token);
      localStorage.setItem('userId', userId);
      
      // Можно добавить коллбек или событие для уведомления о успешном логине
      // Например, через контекст или глобальное состояние
      window.location.reload(); // Обновить страницу или перенаправить пользователя на другую страницу

    } catch (error) {
      setError('Неверный email или пароль');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container maxWidth="xs" sx={{ mt: 8 }}>
      <Typography variant="h5" component="h1" gutterBottom>
        Вход
      </Typography>
      <Box component="form" noValidate autoComplete="off" sx={{ mt: 2 }}>
        <TextField
          fullWidth
          margin="normal"
          label="Email"
          variant="outlined"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <TextField
          fullWidth
          margin="normal"
          label="Пароль"
          type="password"
          variant="outlined"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
        {error && (
          <Typography color="error" sx={{ mt: 2 }}>
            {error}
          </Typography>
        )}
        <Button
          fullWidth
          variant="contained"
          color="primary"
          onClick={handleLogin}
          disabled={loading}
          sx={{ mt: 2 }}
        >
          {loading ? 'Вход...' : 'Войти'}
        </Button>
      </Box>
    </Container>
  );
};

export default Login;
