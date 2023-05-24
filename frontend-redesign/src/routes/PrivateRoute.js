import React, { useContext } from 'react';
import { Navigate } from 'react-router-dom';
import { AuthContext } from '../helpers/AuthContext';
import Loading from '../pages/Loading';

const PrivateRoute = ({ children }) => {
  const { loggedIn } = useContext(AuthContext);

  if (loggedIn === null) {
    return (<Loading/>);
  } else if (!loggedIn) {
    return <Navigate to="/login" replace />;
  }

  // User is authenticated, allow access to the protected route
  return children;
};

export default PrivateRoute;
