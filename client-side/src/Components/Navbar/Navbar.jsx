import React from 'react';
import {Link} from 'react-router-dom'

const Navbar = () => {
    const navLinks =
        [{
            path: "/panel-admina", element: "Panel admina",
        }, {
            path: "/powiadomienia", element: "powiadomienia",
        }, {
            path: "/jadlospis", element: "Menu",
        }, {
            path: "/raporty", element: "Raporty",
        }, {
            path: "/ustawienia", element: "Ustawienia",
        }];
    return (
        <div className="h-[80px] flex justify-center ">
            {navLinks.map(el =>
                (
                    <div key={el.element} className="px-5">
                    <Link to={el.path}>{el.element}</Link>
                    </div>)
            )}
        </div>
    );
};

export default Navbar;