import React from 'react';
import {Route, Routes} from "react-router-dom";
import Dashboard from "../Dashboard";
import AdminPanel from "../AdminPanel";
import Notifications from "../Notifications";
import Menu from "../Menu";
import Raports from "../Raports";
import UserSettings from "../UserSettings";
const ProtectedRoutes = () => {
    return (
        <Routes>
            <Route path={"/dashboard"} element={<Dashboard/>}/>
            <Route path={"/panel-admina"} element={<AdminPanel/>}/>
            <Route path={"/powiadomienia"} element={<Notifications/>}/>
            <Route path={"/jadlospis"} element={<Menu/>}/>
            <Route path={"/raporty"} element={<Raports/>}/>
            <Route path={"/ustawienia"} element={<UserSettings/>}/>
        </Routes>
    );
};

export default ProtectedRoutes;