import React from "react";
import {Route, Routes} from "react-router-dom";
import Login from "./Pages/Login";
import Register from "./Pages/Register";
import Navbar from "./Components/Navbar/Navbar";
import Sidebar from "./Components/Sidebar/Sidebar";
import Dashboard from "./Pages/Dashboard";
import AdminPanel from "./Pages/AdminPanel";
import Notifications from "./Pages/Notifications";
import Menu from "./Pages/Menu";
import Raports from "./Pages/Raports";
import UserSettings from "./Pages/UserSettings";

function App() {

    return (
        <div>
            <Routes>
                <Route path={'/login'} element={<Login/>}/>
                <Route path={'/register'} element={<Register/>}/>
            </Routes>
            <Navbar/>
            <div className='flex'>
                <div className='flex flex-col'>
                    <Sidebar/>
                </div>
                <Routes>
                    <Route path={"/dashboard"} element={<Dashboard/>}/>
                    <Route path={"/panel-admina"} element={<AdminPanel/>}/>
                    <Route path={"/powiadomienia"} element={<Notifications/>}/>
                    <Route path={"/jadlospis"} element={<Menu/>}/>
                    <Route path={"/raporty"} element={<Raports/>}/>
                    <Route path={"/ustawienia"} element={<UserSettings/>}/>
                </Routes>
            </div>
        </div>
    );
}

export default App;
