import React, { useContext, useEffect } from "react";
import Header from "../components/Header";
import TypingText from "../components/TypingText"
import { AuthContext } from "../helpers/AuthContext";
import { useNavigate } from "react-router-dom";


const HomePage = () => {
    const { loggedIn } = useContext(AuthContext);
    const navigate = useNavigate();

    console.log(loggedIn)

    useEffect(() => {
        if(loggedIn)
        navigate('/dashboard')
    }, [loggedIn, navigate])

    return (
        <>
        <Header/>
        <TypingText/>
        </>
    )
}

export default HomePage;