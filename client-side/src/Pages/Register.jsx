import React from 'react'
import RegisterInput from "../Components/RegisterComponents/RegisterInput";

const Register = () => {
    return (
        <div className='h-screen flex justify-center'>
            <form className='flex flex-col self-center'>
                <RegisterInput/>
            </form>
        </div>
    )
}

export default Register