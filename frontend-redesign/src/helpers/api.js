import axios from 'axios';

const baseURL = process.env.REACT_APP_BACKEND_URL;

const api = axios.create({
  baseURL,
});

export const login = async (username, password) => {
  try {
    console.log("entered axios call")
    console.log(baseURL)
    const response = await api.post('/auth/login', { username, password });
    return response.data; // Assuming the response contains the JWT token
  } catch (error) {
    throw error;
  }
};