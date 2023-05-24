import React, { useState, useEffect } from 'react';
import { Button } from '@material-tailwind/react';

const TypingText = () => {
  const [text, setText] = useState('');
  const fullText = 'Manage All Your Tasks In One Place!';
  const typingSpeed = 100; // Adjust typing speed in milliseconds

  useEffect(() => {
    let currentText = '';
    let currentIndex = 0;

    const typeText = () => {
      if (currentIndex < fullText.length) {
        currentText += fullText[currentIndex];
        setText(currentText);
        currentIndex++;
        setTimeout(typeText, typingSpeed);
      }
    };

    typeText();
  }, []);

  return (
    <div className="flex flex-col items-center justify-center h-screen mt-5">
      <h1 className="text-4xl font-bold mb-5">{text}</h1>
      <span className='mb-5'>DoIt is the best place to manage your tasks effortlesly!</span>
      <img src="./tasks.png" alt="Tasks" className='mb-5'/>
      <div className="flex items-center lg:order-2 space-x-4">
            <Button variant='outlined' className='shadow-md bg-white border-0' onClick={()=> 
              window.location = '/register'
            }>Get started</Button>
      </div>
    </div>
  );
};

export default TypingText;
