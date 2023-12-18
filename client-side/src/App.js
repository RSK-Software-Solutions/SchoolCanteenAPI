import React, {useContext} from "react";
import {Navigate, Route, Routes} from "react-router-dom";
import Login from "./Pages/Login";
import Register from "./Pages/Register";
import Navbar from "./Components/Navbar/Navbar";
import Sidebar from "./Components/Sidebar/Sidebar";
import ProtectedRoutes from "./Pages/ProtectedRoutes/ProtectedRoutes";
import {AuthContext} from "./Context/AuthContext";

function App() {
    const getSession = useContext(AuthContext)
    const isLoggedin = getSession.token


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
                {(isLoggedin) ? <ProtectedRoutes/> : <Navigate
                    to={'/login'}/>} {/*this is protected route is user don't have token than redirect to log in*/}
            </div>
        </div>
    );
}

export default App;
