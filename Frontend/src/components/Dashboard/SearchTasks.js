// src/components/Dashboard/SearchTasks.js
import React, { useState } from 'react';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { searchTasks } from '../../utils/api';

function SearchTasks() {
  const [query, setQuery] = useState('');
  const [tasks, setTasks] = useState([]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const result = await searchTasks(query);
    if (result) {
      setTasks(result);
    } else {
      setTasks([]);
    }
  };

  return (
    <div>
      <Typography variant="h6" gutterBottom>
        Search Tasks
      </Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          label="Search"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          required
        />
        <Button color="primary" variant="contained" sx={{ ml: 1 }} type="submit">
          Search
        </Button>
      </form>
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

export default SearchTasks;
