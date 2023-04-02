// src/utils/api.js
const API_BASE_URL = 'https://your-api-base-url.com';

export async function login(email, password) {
  const response = await fetch(`${API_BASE_URL}/login`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ email, password }),
  });

  if (response.ok) {
    const data = await response.json();
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
  const response = await fetch(`${API_BASE_URL}/tasks/daily`);
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