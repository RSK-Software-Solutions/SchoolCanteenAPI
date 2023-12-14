import React from 'react'
import LoginInput from "../Components/LoginComponents/LoginInput";

const Login = () => {
    return (
        <div className='h-screen flex justify-center '>
            <form className='flex flex-col self-center'>
                <LoginInput/>
            </form>
        </div>
    )
}

export default Login