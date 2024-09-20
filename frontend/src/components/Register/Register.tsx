import React, { useState } from 'react';
import { TextField, Button, Container, Typography, Box, Alert } from '@mui/material';
import axios from 'axios';
import { RegisterRequest, RegisterResponse } from '../../shared/types/Models.ts';

const Register = () => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [photoURL, setPhotoURL] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setError(null);
    setSuccess(null);
  
    const requestData: RegisterRequest = {
      name,
      email,
      password,
      photoURL,
      phoneNumber
    };
  
    try {
      const response = await axios.post<RegisterResponse>('https://localhost:7292/api/auth/register', requestData);
      setSuccess('Registration successful!');
      window.location.href = '/login';
    } catch (err: any) {
      setError(err.response?.data?.error || 'An error occurred');
    }
  };

  return (
    <Container maxWidth="sm">
      <Box sx={{ mt: 8 }}>
        <Typography variant="h4" gutterBottom>
          Register
        </Typography>
        <form onSubmit={handleSubmit}>
          <TextField
            label="Name"
            variant="outlined"
            fullWidth
            margin="normal"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
          <TextField
            label="Email"
            variant="outlined"
            fullWidth
            margin="normal"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
          <TextField
            label="Password"
            type="password"
            variant="outlined"
            fullWidth
            margin="normal"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <TextField
            label="Photo URL"
            variant="outlined"
            fullWidth
            margin="normal"
            value={photoURL}
            onChange={(e) => setPhotoURL(e.target.value)}
          />
          <TextField
            label="Phone Number"
            variant="outlined"
            fullWidth
            margin="normal"
            value={phoneNumber}
            onChange={(e) => setPhoneNumber(e.target.value)}
          />
          <Button type="submit" variant="contained" color="primary" fullWidth sx={{ mt: 2 }}>
            Register
          </Button>
          {error && <Alert severity="error" sx={{ mt: 2 }}>{error}</Alert>}
          {success && <Alert severity="success" sx={{ mt: 2 }}>{success}</Alert>}
        </form>
      </Box>
    </Container>
  );
};

export default Register;
