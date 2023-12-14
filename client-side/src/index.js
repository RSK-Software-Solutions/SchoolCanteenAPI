import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import Register from './Pages/Register';
import Login from './Pages/Login';
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Navbar from "./Components/Navbar/Navbar";
import Sidebar from "./Components/Sidebar/Sidebar";
import Dashboard from "./Pages/Dashboard";
import AdminPanel from "./Pages/AdminPanel";
import Notifications from "./Pages/Notifications";
import Menu from "./Pages/Menu";
import Raports from "./Pages/Raports";
import UserSettings from "./Pages/UserSettings";


const router = ([{
    path: "/dashboard", element: <Dashboard/>,
}, {
    path: "/panel-admina", element: <AdminPanel/>,
}, {
    path: "/powiadomienia", element: <Notifications/>,
}, {
    path: "/jadlospis", element: <Menu/>,
}, {
    path: "/raporty", element: <Raports/>,
}, {
    path: "/ustawienia", element: <UserSettings/>,
}]);

ReactDOM.createRoot(document.getElementById("root")).render(<React.StrictMode>
    <BrowserRouter>
        <Routes>
            <Route path={'/login'} element={<Login/>}/>
            <Route path={'/register'} element={<Register/>}/>
        </Routes>
        <Navbar/>
        <div className='flex'>
            <Sidebar/>
            <Routes>
                {router.map(routes => (<Route path={routes.path} element={routes.element}/>))}
            </Routes>
        </div>
    </BrowserRouter>
</React.StrictMode>);

