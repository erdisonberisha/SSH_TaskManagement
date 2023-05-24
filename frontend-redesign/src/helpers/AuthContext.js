import React, { createContext, useEffect, useState } from 'react';
import axios from 'axios';

const token = localStorage.getItem('token');
const baseURL = process.env.REACT_APP_BACKEND_URL;

const instance = axios.create({
    baseURL,
    headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
    }
});

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [loggedIn, setLoggedIn] = useState(null);

    useEffect(() => {
        if (token) {
            verifyToken(token)
                .then(() => {
                    setLoggedIn(true);
                })
                .catch(() => {
                    setLoggedIn(false);
                });
        } else {
            setLoggedIn(false);
        }
    }, []);

    const verifyToken = async (token) => {
        try {
            const response = await instance.get('/auth', null);

            if (response.status === 200) {
                return response.data;
            } else {
                throw new Error('Invalid token');
            }
        } catch (error) {
            throw new Error('Invalid token');
        }
    };
    
    return (
        <AuthContext.Provider value={{ loggedIn, setLoggedIn }}>
            {children}
        </AuthContext.Provider>
    );
};
