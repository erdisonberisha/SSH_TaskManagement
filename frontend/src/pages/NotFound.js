import React from "react";
import Header from "../components/homepage/Header";
import { Button } from "@material-tailwind/react";

const NotFound = () => {
    return (
        <>
        <Header/>
        <div className="flex flex-col items-center justify-center h-screen mt-5">
            <h1 className="text-4xl font-bold mb-5">404 NOT FOUND!</h1>
            <span>The page you were asking for was not found</span>
            <img src="./notfound.svg" alt="Tasks" className='mb-5 h-2/4'/>
            <div className="flex items-center lg:order-2 space-x-4">
                  <Button variant='outlined' className='shadow-md bg-white border-0' onClick={()=> 
                    window.location = '/'
                  }>Go to home</Button>
            </div>
        </div>
    </>
    )
}

export default NotFound;