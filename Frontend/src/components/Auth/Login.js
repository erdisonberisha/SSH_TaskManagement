// src/components/Auth/Login.js
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, TextField, Typography, Container, Paper } from '@mui/material';
import { login } from '../../utils/api';
import { toast } from 'react-toastify';

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate;

  const handleSubmit = async (e) => {
    e.preventDefault();
    const result = await login(username, password);
    if (result) {
      // Save token and user info to localStorage or context
      navigate('/');
      toast.success('Logged in successfully');
    } else {
      toast.error('Login failed');
    }
  };

  return (
    <Container maxWidth="xs">
      <Paper sx={{ p: 4, mt: 8 }}>
        <Typography variant="h5" gutterBottom>
          Login
        </Typography>
        <form onSubmit={handleSubmit}>
          <TextField
            fullWidth
            margin="normal"
            label="Username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
          <TextField
            fullWidth
            margin="normal"
            label="Password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <Button fullWidth color="primary" variant="contained" sx={{ mt: 2 }} type="submit">
            Login
          </Button>
        </form>
      </Paper>
    </Container>
  );
}

export default Login;