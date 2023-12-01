import React from 'react'

const Register = () => {
    const Credentials = [
        {
            text: "Firma"
        },
        {
            text: "Email"
        },
        {
            text: "Hasło"
        }
    ]

    return (
        <div className='bg-main-register h-screen flex flex-col md:flex-row justify-center gap-8 md:gap-28 max-sm:flex-col-reverse max-md:flex-col-reverse'>
            {/* 1. Information if the user has an account */}
            <div className='self-center h-[200px] mb-4 md:mb-0 info shadow-md flex items-center roboto-mono flex-col md:pt-12 md:mx-12'>
        <span className='mx-4 md:mx-10 text-center max-md:pt-8'>
            Jeżeli posiadasz konto<br/> w naszym serwisie to <br/> zaloguj się
        </span>
                <button className='btn my-5 mx-4 shadow-md roboto-mono'>Zaloguj</button>
            </div>

            {/* 2. Register form */}
            <div className='self-center h-[300px] form shadow-md w-full md:w-fit md:mr-12 flex items-center justify-center roboto-mono max-md:w-[250px] max-sm:[250px]'>
                <form className='form flex flex-col items-center'>
                    <span className='pt-3 roboto-mono'>Logowanie</span>
                    {Credentials.map(creds => (
                        <div key={creds.text} className='mt-3 mx-3 roboto-mono'>
                            {creds.text}
                            <input type="text" className='input-credentials flex flex-col'/>
                        </div>
                    ))}
                    <button className='btn my-5 shadow-md roboto-mono'>Zarejestruj</button>
                </form>
            </div>
        </div>
    )
}

export default Register