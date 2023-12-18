import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import {BrowserRouter} from "react-router-dom";
import {AuthContextProvider} from "./Context/AuthContext";
import App from "./App";

//todo: authProvider, auth-context ???? refactor code login register to useReducer

ReactDOM.createRoot(document.getElementById("root")).render(
    <React.StrictMode>
        <BrowserRouter>
            <AuthContextProvider>
                <App/>
            </AuthContextProvider>
        </BrowserRouter>
    </React.StrictMode>
);

