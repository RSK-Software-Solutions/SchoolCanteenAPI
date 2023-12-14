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
        <div>
            {navLinks.map(el =>
                (
                    <Link to={el.path}>{el.element}</Link>
                )
            )}
        </div>
    );
};

export default Navbar;