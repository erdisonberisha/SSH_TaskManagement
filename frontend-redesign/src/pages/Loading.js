import React from 'react';
import { Spinner } from '@material-tailwind/react';

const Loading = () => {
  return (
    <div className="flex justify-center items-center h-screen">
      <Spinner className="h-12 w-12" />
    </div>
  );
};

export default Loading;
