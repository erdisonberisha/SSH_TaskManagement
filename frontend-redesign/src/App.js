import './App.css';
import HomePage from './pages/HomePage';
import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
import NotFound from './pages/NotFound';
import Login from './pages/Login';
import Register from './pages/Register';
import Dashboard from './pages/Dashboard';
import { AuthProvider } from './helpers/AuthContext';


const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path="/" element={<HomePage />} />
      <Route path="/login" element={<Login />} />
      <Route path="/dashboard" element={<Dashboard />} />
      <Route path="/register" element={<Register />} />
      <Route path="*" element={<NotFound />} />
    </>
  )
);

function App() {
  return (
    <AuthProvider>
      <RouterProvider router={router} />
    </AuthProvider>
  );
}

export default App;
