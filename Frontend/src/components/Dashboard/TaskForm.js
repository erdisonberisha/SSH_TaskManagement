// src/components/Dashboard/TaskForm.js
import React, { useState } from 'react';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { createTask } from '../../utils/api';
import { toast } from 'react-toastify';

function TaskForm() {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    const result = await createTask({ title, description });
    if (result) {
      setTitle('');
      setDescription('');
      toast.success('Task created successfully');
      // Refresh the task list, using context or callback function
    } else {
      toast.error('Task creation failed');
    }
  };

  return (
    <div>
      <Typography variant="h6" gutterBottom>
        Add Task
      </Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          fullWidth
          margin="normal"
          label="Task title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          required
        />
        <TextField
          fullWidth
          margin="normal"
          label="Task description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          required
        />
        <Button fullWidth color="primary" variant="contained" sx={{ mt: 2 }} type="submit">
          Add Task
        </Button>
      </form>
    </div>
  );
}

export default TaskForm;
