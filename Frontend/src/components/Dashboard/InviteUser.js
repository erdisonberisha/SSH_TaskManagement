// src/components/Dashboard/InviteUser.js
import React, { useState } from 'react';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { inviteUserToTask } from '../../utils/api';
import { toast } from 'react-toastify';

function InviteUser() {
  const [email, setEmail] = useState('');
  const [taskId, setTaskId] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    const result = await inviteUserToTask(taskId, email);
    if (result) {
      setEmail('');
      setTaskId('');
      toast.success('User invited successfully');
    } else {
      toast.error('Failed to invite user');
    }
  };

  return (
    <div>
      <Typography variant="h6" gutterBottom>
        Invite User to Task
      </Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          label="Task ID"
          value={taskId}
          onChange={(e) => setTaskId(e.target.value)}
          required
        />
        <TextField
          label="Email"
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <Button color="primary" variant="contained" sx={{ ml: 1 }} type="submit">
          Invite User
        </Button>
      </form>
    </div>
  );
}

export default InviteUser;