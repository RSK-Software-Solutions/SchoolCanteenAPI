import React from 'react';
import {Link} from 'react-router-dom'


const Navbar = () => {
    const navLinks = [
        {
            path: "/panel-admina", element: "Panel admina",
        },
        {
            path: "/powiadomienia", element: "powiadomienia",
        },
        {
            path: "/jadlospis", element: "Menu",
        },
        {
            path: "/raporty", element: "Raporty",
        },
        {
            path: "/ustawienia", element: "Ustawienia",
        }
    ];
    return (
        <div className='h-[80px] border-b flex'>
            <span className='flex self-center justify-start'>logo</span>
            <div className='w-full flex justify-center'>
                {navLinks.map(el => (
                    <div key={el.path} className="px-5 flex justify-center self-center">
                        <Link to={el.path}>{el.element}</Link>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Navbar;