import React, {useState} from 'react';
import {AnimatePresence, motion} from "framer-motion";
import {PanelLeftClose, PanelRightClose} from "lucide-react";

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
                            {isOpen && (
                                <section
                                    className='flex flex-col h-screen w-[150px] border-r justify-center '>
                                    <nav className='w-full flex justify-center'>
                                        MENU
                                    </nav>
                                </section>)}

                        </motion.section>
                    ))}
                </AnimatePresence>
            )
            }

            export default Sidebar;