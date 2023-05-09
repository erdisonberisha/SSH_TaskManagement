import { useState, useEffect } from 'react';
import { Button } from "@material-tailwind/react";
import { login } from '../../helpers/api';
import swal from 'sweetalert';
import { useNavigate } from 'react-router-dom';


const LoginForm = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    if (isLoggedIn) {
      navigate('/');
    }
  }, [isLoggedIn, navigate]);

  const handleUsernameChange = (event) => {
    setUsername(event.target.value);
  };

  const handlePasswordChange = (event) => {
    setPassword(event.target.value);
  };

  const handleSubmit = async(event) => {
    event.preventDefault();

    if (username.length <= 4) {
      swal({
        title: "Username is invalid!",
        text: "Username length should be more than 4!",
        icon: "warning",
        timer: 2000,
        buttons: false
    });
      return;
    }

    if (password.length < 8) {
      swal({
        title: "Password is invalid!",
        text: "Password length should be more than 7!",
        icon: "warning",
        timer: 2000,
        buttons: false
    });
      return;
    }
    try {
      const response = await login(username, password);
      localStorage.setItem('token', response.token);
      setIsLoggedIn(true);
      navigate('/');
    } catch (error) {
      setError('Invalid username or password.');
    }
  };

  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <div className="bg-white rounded-md shadow-md text-center p-20">
        <h1 className="text-4xl font-bold mb-10">Log in</h1>
        <form className="flex flex-col items-center space-y-4" onSubmit={handleSubmit}>
          {error && <p className="text-red-500">{error}</p>}
          <input
            type="text"
            placeholder="Username"
            className="px-4 py-2 rounded-md border border-gray-300"
            value={username}
            onChange={handleUsernameChange}
          />
          <input
            type="password"
            placeholder="Password"
            className="px-4 py-2 rounded-md border border-gray-300"
            value={password}
            onChange={handlePasswordChange}
          />
          <Button type="submit" variant="outlined" className="shadow-md">
            LOGIN
          </Button>
        </form>
      </div>
    </div>
  );
}

export default LoginForm;