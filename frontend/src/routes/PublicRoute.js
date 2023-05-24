import React, { useContext } from 'react';
import { Navigate } from 'react-router-dom';
import { AuthContext } from '../helpers/AuthContext';
import Loading from '../pages/Loading';

const PublicRoute = ({ children }) => {
    
  const { loggedIn } = useContext(AuthContext);


  if (loggedIn === null) {
    return (<Loading/>);
  } else if (loggedIn) {
      return <Navigate to="/dashboard" replace />;
    }
  
    return children;
  };

export default PublicRoute;