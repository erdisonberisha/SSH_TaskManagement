import axios from 'axios';

const baseURL = process.env.REACT_APP_BACKEND_URL;

const api = axios.create({
  baseURL,
  headers: {
    'Content-Type': 'application/json'
  }
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
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

export const getAutocomplete = async (query) => {
  try {
    const response = await api.get('/tasks/autocomplete', {
      params: {
        query: query // Add the query parameter
      }
    });
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const searchTasks = async (query, status, priority) => {
  try {
    const params = {
      Query: query,
      Status: status,
      Priority: priority
    };

    const response = await api.get('/tasks/search', { params });
    return response.data;
  } catch (error) {
    throw error;
  }
};



export const updateTaskStatus = async (taskId, status) => {
  try {
    const patchDocument = [
      {
        op: 'replace',
        path: '/status',
        value: status,
      },
    ];
    const response = await api.patch(`/tasks/${taskId}`, patchDocument);
    return response.data;
  } catch (error) {
    throw new Error('Error updating task status');
  }
};

export const createTask = async (taskData) => {
  try {
    const response = await api.post(`/tasks`, taskData);
    return response.data;
  } catch (error) {
    throw new Error('Error updating task status');
  }
};

export const deleteTask = async (taskId) => {
  try {
    const response = await api.delete(`/tasks`, {
      params: {
        id: taskId
      }});
    return response.data;
  } catch (error) {
    throw new Error('Error updating task status');
  }
};