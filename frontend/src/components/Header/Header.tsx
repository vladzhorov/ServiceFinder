import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { FaUserCircle, FaSearch } from 'react-icons/fa';
import { AppBar, Toolbar, Typography, IconButton, Button, TextField, Box, Avatar, InputAdornment, Modal, Container } from '@mui/material';
import axios from 'axios';
import Login from '../Login/Login.tsx';
import Register from '../Register/Register.tsx';

const Header = () => {
  const [user, setUser] = useState<any>(null);
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [openLoginModal, setOpenLoginModal] = useState<boolean>(false);
  const [openRegisterModal, setOpenRegisterModal] = useState<boolean>(false);

  useEffect(() => {
    const token = localStorage.getItem('authToken');
    const userId = localStorage.getItem('userId');
    if (token && userId) {
      axios.get(`http://localhost:7091/api/usersProfile/${userId}`, { headers: { Authorization: `Bearer ${token}` } })
        .then(response => {
          setUser(response.data);
          setIsAuthenticated(true);
        })
        .catch(() => setIsAuthenticated(false));
    }
  }, []);

  const handleLogout = () => {
    localStorage.removeItem('authToken');
    localStorage.removeItem('userId');
    setUser(null);
    setIsAuthenticated(false);
  };

  return (
    <AppBar position="static" color="primary" sx={{ mb: 2 }}>
      <Toolbar>
        <Typography
          variant="h6"
          component={Link}
          to="/"
          sx={{ flexGrow: 1, textDecoration: 'none', color: 'inherit' }}
        >
          ZhorovAssistances
        </Typography>

        <Box sx={{ display: 'flex', alignItems: 'center', flexGrow: 1 }}>
          <TextField
            variant="outlined"
            placeholder="Поиск..."
            size="small"
            sx={{ mr: 2, width: '300px' }}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <FaSearch />
                </InputAdornment>
              ),
            }}
          />
          <Button component={Link} to="/catalog" color="inherit">
            Каталог
          </Button>
          <Button component={Link} to="/" color="inherit">
            Главная
          </Button>
        </Box>

        {isAuthenticated ? (
          <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <IconButton component={Link} to="/profile" color="inherit" sx={{ mr: 1 }}>
              <Avatar src={user?.photoURL || '/default-avatar.png'} alt={user?.name || 'User'} />
            </IconButton>
            <Typography
              component={Link}
              to="/profile"
              variant="body1"
              sx={{ color: 'inherit', textDecoration: 'none', mr: 2 }}
            >
              {user?.name || 'User'}
            </Typography>
            <Button onClick={handleLogout} color="inherit">
              Выход
            </Button>
            <Button
              component={Link}
              to={`/create-assistance?userProfileId=${user?.id || ''}`}
              variant="contained"
              color="secondary"
              sx={{ ml: 2 }}
            >
              Создать Объявление
            </Button>
          </Box>
        ) : (
          <>
            <Button onClick={() => setOpenLoginModal(true)} color="inherit" startIcon={<FaUserCircle />}>
              Вход
            </Button>
            <Button onClick={() => setOpenRegisterModal(true)} color="inherit">
              Регистрация
            </Button>
          </>
        )}
      </Toolbar>

      <Modal open={openLoginModal} onClose={() => setOpenLoginModal(false)}>
        <Container maxWidth="sm" sx={{ mt: 8, mb: 4, p: 3, backgroundColor: 'white' }}>
          <Login />
        </Container>
      </Modal>

      <Modal open={openRegisterModal} onClose={() => setOpenRegisterModal(false)}>
        <Container maxWidth="sm" sx={{ mt: 8, mb: 4, p: 3, backgroundColor: 'white' }}>
          <Register />
        </Container>
      </Modal>
    </AppBar>
  );
};

export default Header;
