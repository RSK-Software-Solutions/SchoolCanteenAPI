import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import Register from './Pages/Register';
import Login from './Pages/Login';
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";

const router = createBrowserRouter([
    {
        path: "/register",
        element: <Register/>,
    },
    {
        path: "/login",
        element: <Login/>,
    }
]);

ReactDOM.createRoot(document.getElementById("root")).render(
    <React.StrictMode>
        <font className="font-class">
            <RouterProvider
                router={router}
            />
        </font>
    </React.StrictMode>
);

