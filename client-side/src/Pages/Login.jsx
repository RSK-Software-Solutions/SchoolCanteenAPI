import React, {useState} from 'react'
import LoginInput from "../Components/LoginInput";
import {Link} from "react-router-dom";

const Login = () => {


  return (
      <div
          className='bg-main-register h-screen flex flex-col md:flex-row justify-center gap-8 md:gap-28 max-sm:flex-col-reverse max-md:flex-col-reverse'>
        {/* 1. Information if the user don't have acc */}
        <div
            className='self-center h-[200px] mb-4 md:mb-0 info shadow-md flex items-center  flex-col md:pt-12 md:mx-12'>
        <span className='mx-4 md:mx-10 text-center max-md:pt-8'>
            Logowanie do systemu <br/> możliwe jest tylko <br/> dla Firm <br/> posiadających konto.
        </span>
          <Link to={'/register'} className='btn my-5 mx-4 shadow-md'>Zarejestruj się</Link>
        </div>

        {/* 2. Login form */}
        <div
            className='self-center h-[300px] form-class shadow-md w-full md:w-fit md:mr-12 flex items-center justify-center  max-md:w-[250px] max-sm:[250px]'>
          <form className='form-class flex flex-col items-center'>
            <span className='pt-3 '>Logowanie</span>
            <LoginInput/>
            <button className='btn my-5 shadow-md '>Zarejestruj</button>
          </form>
        </div>
      </div>
  )
}

export default Login