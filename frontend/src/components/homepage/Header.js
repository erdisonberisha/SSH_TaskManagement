import React from 'react';
import { Button } from "@material-tailwind/react";
const Header = () => {
  return (
    <header>
      <nav className="bg-gray=100 border-gray-100 px-4 lg:px-6 py-2.5 dark:bg-gray-900 fixed left-0 right-0 top-0">
        <div className="flex flex-wrap justify-between items-center mx-auto max-w-screen-xl">
          <a href="/" className="flex items-center">
            <img
              src="./sticky-notes.png"
              className="mr-3 h-6 sm:h-9"
              alt="Flowbite Logo"
            />
            <span className="self-center text-xl font-semibold whitespace-nowrap dark:text-white">
              DoIt
            </span>
          </a>
          <div className="flex items-center lg:order-2 space-x-4 button-wrapper">
            <Button variant='text' className='text-black' onClick={()=> 
              window.location = '/login'
            }>Login</Button>
            <Button variant='outlined' className='shadow-md' onClick={()=> 
              window.location = '/register'
            }>Register</Button>
      </div>
    </div>
  </nav>
</header>
);
};

export default Header;