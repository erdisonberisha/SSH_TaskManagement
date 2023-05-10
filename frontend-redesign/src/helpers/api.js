import axios from 'axios';

const baseURL = process.env.REACT_APP_BACKEND_URL;

const api = axios.create({
  baseURL,
});

export const login = async (username, password) => {
  try {
    const response = await api.post('/auth/login', { username, password });
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const register = async ( name, email, username, birthday, password) => {
  try {
    const response = await api.post('/auth/register', { name, email, username, birthday, password });
    return response.data;
  } catch (error) {
    throw error;
  }
};