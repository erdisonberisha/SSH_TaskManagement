import axios from 'axios';

const token = localStorage.getItem('token');
const baseURL = process.env.REACT_APP_BACKEND_URL;

const api = axios.create({
  baseURL,
  headers: {
    'Content-Type': 'application/json',
    Authorization: `Bearer ${token}`,
  }
});

export const login = async (username, password) => {
  try {
    const response = await api.post('/auth/login', { username, password });
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const register = async (name, email, username, birthday, password) => {
  try {
    const response = await api.post('/auth/register', { name, email, username, birthday, password });
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const getTasks = async () => {
  try {
    const response = await api.get('/tasks');
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const updateTaskStatus = async (taskId, newStatus) => {
  try {
    const response = await axios.patch(`/tasks/${taskId}`, { status: newStatus });
    return response.data;
  } catch (error) {
    throw new Error('Error updating task status');
  }
};