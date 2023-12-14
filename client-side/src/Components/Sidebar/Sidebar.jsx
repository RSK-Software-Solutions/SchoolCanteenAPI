import React, {useRef, useState} from 'react';
import {motion} from "framer-motion";
import {AlignJustify} from "lucide-react";
const Sidebar = () => {
    const menuRef = useRef(null);
    const [isOpen, setIsOpen] = useState(false)

    const handleMenuToggle = () => {
        setIsOpen((prevState) => !prevState);
    };

    return (
        <div>
            <span className='max-sm:block max-2xl:hidden'>
                <AlignJustify onClick={handleMenuToggle} className='flex justify-end'/>
            </span>
            {isOpen && ((
                <motion.section
                    ref={menuRef}
                    className="menu"
                    initial={{ opacity: 0, x: '-100%' }}
                    animate={{ opacity: 1, x: 0 }}
                    transition={{ duration: 0.5 }}
                >
                    <div className='flex flex-col h-screen w-[150px] border'>
                        Menu
                    </div>
                </motion.section>
            ))}
        </div>
    )
}

export default Sidebar;