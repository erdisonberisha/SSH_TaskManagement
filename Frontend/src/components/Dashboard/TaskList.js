// src/components/Dashboard/TaskList.js
import React, { useState, useEffect } from 'react';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import { getTasks } from '../../utils/api';

function TaskList() {
  const [tasks, setTasks] = useState([]);

  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    const fetchedTasks = await getTasks();
    setTasks(fetchedTasks);
  };

  return (
    <div>
      <Typography variant="h6" gutterBottom>
        All Tasks
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

export default TaskList;
