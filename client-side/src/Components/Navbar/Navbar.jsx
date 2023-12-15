import React from 'react';
import {Link} from 'react-router-dom'
import {AnimatePresence, motion} from "framer-motion";
import {navLinks} from "./NavbarLinks";


const Navbar = () => {

    return (
        <div className='h-[80px] border-b flex'>
            <AnimatePresence>
                <motion.span className='flex self-center justify-start'
                             whileHover={{ scale: 1.2 }}
                             whileTap={{ scale: 0.8 }}
                             style={{ x: 100 }}
                >
                    <Link to={'/dashboard'}>logo</Link>
                </motion.span>
            </AnimatePresence>
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