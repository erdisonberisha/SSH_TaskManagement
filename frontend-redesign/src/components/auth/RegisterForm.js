import { useState } from 'react';
import { Button } from "@material-tailwind/react";
import { register } from '../../helpers/api';
import swal from 'sweetalert';
import { useNavigate } from 'react-router-dom';


const RegisterForm = () => {
    const [name, setName] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [birthday, setBirthday] = useState('');
    const [inputType, setInputType] = useState('text');
    const [password2, setPassword2] = useState('');
    const [email, setEmail] = useState('');
    const navigate = useNavigate();

    console.log(email);

    const handleInputFocus = () => {
        setInputType('date');
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (name.length <= 2) {
            swal({
                title: "Name is invalid!",
                text: "Name length should be more than 2!",
                icon: "warning",
                timer: 2000,
                buttons: false
            });
            return;
        }

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
        if (password !== password2) {
            swal({
                title: "Passwords don't match",
                text: "Password you have entered don't match",
                icon: "warning",
                timer: 2000,
                buttons: false
            });
            return;
        }
        if (birthday) {
            const today = new Date();
            const selectedDate = new Date(birthday);
            const twelveYearsAgo = new Date();
            twelveYearsAgo.setFullYear(today.getFullYear() - 12);

            if (selectedDate > twelveYearsAgo) {
                swal({
                    title: "Age not allowed",
                    text: "You must be older than 12 to use our platform",
                    icon: "warning",
                    timer: 2000,
                    buttons: false
                });
                return;
            }
        }
        try {
            await register(name, email, username, birthday, password);
            swal({
                title: "Register successful!",
                text: "Welcome to DoIt!",
                icon: "success",
                timer: 2000,
                buttons: false
            });
            navigate('/login');
        } catch (error) {
            swal({
                title: "Register failed!",
                text: error?.response?.data?.message ?? "Error on server connection!",
                icon: "error",
                timer: 2000,
                buttons: false
            });
        }
    };

return (
    <div className="flex flex-col items-center justify-center h-screen mt-5">
        <div className="text-center flex bg-cyan-600 rounded-md lg:pl-24">
            <div className="flex-1 items-center md:mr-20 lg:mr-0 hidden lg:flex">
                <img src="./registerImage.png" alt="registerimage" />
            </div>
            <div className='flex-1'>
                <div className="bg-white rounded-md shadow-md text-center p-20 md:p-12 lg:p-20">
                    <h1 className="text-4xl font-bold mb-10">Register</h1>
                    <form className="flex flex-col items-center space-y-4" onSubmit={handleSubmit}>
                            <input
                                type="text"
                                placeholder="Name"
                                className="px-4 py-2 rounded-md border border-gray-300"
                                value={name}
                                onChange={e => setName(e.target.value)}
                            />
                            <input
                                type="email"
                                placeholder="Email"
                                className="px-4 py-2 rounded-md border border-gray-300"
                                value={email}
                                onChange={e => setEmail(e.target.value)}
                            />
                            <input
                                type="text"
                                placeholder="Username"
                                className="px-4 py-2 rounded-md border border-gray-300"
                                value={username}
                                onChange={e => setUsername(e.target.value)}
                            />
                            <input
                                type="password"
                                placeholder="Password"
                                className="px-4 py-2 rounded-md border border-gray-300"
                                value={password}
                                onChange={e => setPassword(e.target.value)}
                            />
                            <input
                                type="password"
                                placeholder="Confirm password"
                                className="px-4 py-2 rounded-md border border-gray-300"
                                value={password2}
                                onChange={e => setPassword2(e.target.value)}
                            />
                            <input
                                type={inputType}
                                placeholder="Birthday"
                                className="px-4 py-2 rounded-md border border-gray-300"
                                value={birthday}
                                onChange={e => setBirthday(e.target.value)}
                                onFocus={handleInputFocus}
                            />
                            <Button type="submit" variant="outlined" className="shadow-md !mt-10">
                                SIGN UP
                            </Button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default RegisterForm;