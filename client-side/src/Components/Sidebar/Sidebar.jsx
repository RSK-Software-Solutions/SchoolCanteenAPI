import React, {useState} from 'react';
import {AnimatePresence, motion} from "framer-motion";
import {PanelLeftClose, PanelRightClose} from "lucide-react";
import sidebarLinks from "./SidebarLinks";
import {Link} from 'react-router-dom'
import {navLinks} from "../Navbar/NavbarLinks";

const Sidebar = () => {
    const [isOpen, setIsOpen] = useState(false)
    const handleMenuToggle = () => {
        setIsOpen((prevState) => !prevState);
    };

    return (
        <AnimatePresence>
            <motion.div
                animate={{
                    transform: [{translateX: isOpen ? '100%' : '0%'}],
                    transition: {type: "just", ease: 'easeInOut'},
                }}
            >
                <div className='flex justify-end ' onClick={handleMenuToggle}>
                    {isOpen ? <PanelLeftClose/> : <PanelRightClose/>}
                </div>
            </motion.div>
            {isOpen && ((
                <motion.section
                    key={Math.random()}
                    initial={{x: '-100%'}}
                    exit={{x: "-100%"}}
                    animate={{x: 0}}
                    transition={{type: "just", ease: "easeInOut"}}
                >
                    <section
                        className='flex flex-col w-[200px] border-r justify-center '>
                        <nav className='w-full flex justify-center flex-col gap-y-5'>
                            {sidebarLinks.map(sidebarContent => (
                                <Link key={sidebarContent.main} className='flex hover:bg-gray-300 hover:underline pl-5 py-2 text-lg'
                                      to={sidebarContent.link}>{sidebarContent.main}</Link>

                            ))}
                            {navLinks.map(navOnMobile =>
                                (
                                    <Link key={navOnMobile.element} to={navOnMobile.path} className='flex hover:bg-gray-300  hover:underline pl-5 py-2 text-lg max-2xl:hidden max-sm:block'>
                                        {navOnMobile.element}
                                    </Link>
                                ))}
                            <nav>
                            </nav>
                        </nav>
                    </section>
                </motion.section>
            ))}
        </AnimatePresence>
    )
}

export default Sidebar;