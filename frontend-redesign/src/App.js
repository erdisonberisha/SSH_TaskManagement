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


const router = createBrowserRouter(
  createRoutesFromElements(
    <>
    <Route path="/" element={<HomePage/>} />
    <Route path="/login" element={<Login/>} />
    <Route path="*" element={<NotFound />} />
    </>
  )
);

function App() {
  return (
    <>
    <RouterProvider router={router} />
    </>
  );
}

export default App;
