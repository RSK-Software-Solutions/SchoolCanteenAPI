import React from 'react';

const RegisterInput = () => {
    const Credentials = [
        {
            text: "Email"
        },
        {
            text: "Hasło"
        }
    ]

    return (
        <div>
            {Credentials.map(creds => (
                <div key={creds.text} className='mt-3 mx-3 '>
                    {creds.text}
                    <input type="text" className='input-credentials flex flex-col'/>
                </div>
            ))}
        </div>
    );
};

export default RegisterInput;