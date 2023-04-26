// src/utils/api.js

const API_BASE_URL = 'https://localhost:7183/api';

export async function login(username, password) {
  const response = await fetch(`${API_BASE_URL}/auth/login`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ username, password }),
  });

  if (response.ok) {
    const data = await response.json();
    const token = data.token;
    localStorage.setItem('token', token);
    // set user as logged in
    // ...

    return data;
  } else {
    return null;
  }
}

export async function register(email, password) {
  const response = await fetch(`${API_BASE_URL}/register`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ email, password }),
  });

  return response.ok;
}

export async function getDailyTasks() {
  const token = localStorage.getItem('token');
  const response = await fetch(`${API_BASE_URL}/tasks`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
  const data = await response.json();
  return data;
}

export async function searchTasks(query) {
  const response = await fetch(`${API_BASE_URL}/tasks/search?query=${encodeURIComponent(query)}`);
  const data = await response.json();
  return data;
}

export async function createTask(task) {
  const response = await fetch(`${API_BASE_URL}/tasks`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(task),
  });

  if (response.ok) {
    const data = await response.json();
    return data;
  } else {
    return null;
  }
}

export async function getTasks() {
  const response = await fetch(`${API_BASE_URL}/tasks`);
  const data = await response.json();
  return data;
}

export async function inviteUserToTask(taskId, email) {
  const response = await fetch(`${API_BASE_URL}/tasks/${taskId}/invite`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ email }),
  });

  return response.ok;
}