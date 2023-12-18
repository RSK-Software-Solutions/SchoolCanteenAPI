import React from 'react';
import {Link} from 'react-router-dom'
import {navLinks} from "./NavbarLinks";


const Navbar = () => {

    return (
        <div className='h-[80px] border-b flex text-xl'>
            <Link to={'/dashboard'} className='flex self-center ml-20 max-md:ml-10 max-sm:w-full max-sm:mx-0 max-sm:justify-center'>logo</Link>
            <div className=' flex justify-evenly w-full max-sm:hidden'>
                {navLinks.map(el => (
                    <div key={el.path} className=' flex max-sm:hidden'>
                        <div className="flex self-center">
                            <Link to={el.path}>{el.element}</Link>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Navbar;