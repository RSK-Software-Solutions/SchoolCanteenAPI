import React, {useState} from 'react';
import {AnimatePresence, motion} from "framer-motion";
import {AlignJustify} from "lucide-react";

const Sidebar = () => {
    const [isOpen, setIsOpen] = useState(false);

    const handleMenuToggle = () => {
        setIsOpen((prevState) => !prevState);
    };

    return (
        <AnimatePresence>
            <div>
                 <AlignJustify onClick={handleMenuToggle} className='flex flex-col justify-end'/>
            </div>
                {isOpen && ((
                    <motion.section
                        key={Math.random()}
                        initial={{opacity: 0, x: '-100%'}}
                        animate={{opacity: 1, x: 0}}
                        transition={{duration: 0.5}}
                    >
                        <div className='flex flex-col h-screen w-[150px] border-r'>
                            MENU
                        </div>
                    </motion.section>
                ))}
        </AnimatePresence>
    )
}

export default Sidebar;