// src/components/Dashboard/DailyTasks.js
import React, { useState, useEffect } from 'react';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import { getDailyTasks } from '../../utils/api';

function DailyTasks() {
  const [tasks, setTasks] = useState([]);

  useEffect(() => {
    fetchDailyTasks();
  }, []);

  const fetchDailyTasks = async () => {
    const fetchedTasks = await getDailyTasks();
    setTasks(fetchedTasks);
  };

  return (
    <div>
      <Typography variant="h6" gutterBottom>
        Today's Tasks
      </Typography>
      <List>
        {tasks.map((task) => (
          <ListItem key={task.id}>
            {task.title}
          </ListItem>
        ))}
      </List>
    </div>
  );
}

export default DailyTasks;
