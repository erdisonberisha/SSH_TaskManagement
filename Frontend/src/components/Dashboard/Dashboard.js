// src/components/Dashboard/Dashboard.js
import React from 'react';
import Container from '@mui/material/Container';
import DailyTasks from './DailyTasks';
import SearchTasks from './SearchTasks';
import TaskForm from './TaskForm';
import TaskList from './TaskList';
import InviteUser from './InviteUser';

function Dashboard() {
  return (
    <Container maxWidth="md">
      <TaskForm />
      <DailyTasks />
      <SearchTasks />
      <TaskList />
      <InviteUser />
    </Container>
  );
}

export default Dashboard;
