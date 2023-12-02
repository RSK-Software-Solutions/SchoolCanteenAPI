import React, {useState, useEffect} from 'react';
import axios from "axios";

const RegisterInput = () => {
    const [formData, setFormData] = useState({
        Name: '',
        Login: '',
        Password: ''
    });

    const formFields = [
        {
            label: "Firma",
            key: "Name"
        },
        {
            label: "Email",
            key: "Login"
        },
        {
            label: "Hasło",
            key: "Password"
        }
    ];

    const handleChange = (e, field) => {
        setFormData({
            ...formData,
            [field]: e.target.value
        });
    };

    const handleSubmit = async(e) => {
         try {
            e.preventDefault();
            console.log(formData)

            const response = await fetch("https://localhost:7093/api/Company", {
                method: "POST", 
                body: JSON.stringify(formData),
                headers: {"Content-Type": "application/json"}
            })
             let jsonData = response.json();

            console.log(jsonData);
        } catch (error) {
            
            console.error('Error:', error.message);
        }
    };


    return (
        <div >
            {formFields.map(field => (
                <div key={field.key} className='mt-3 mx-3'>
                    <label>{field.label}</label>
                    <input
                        type="text"
                        className='input-credentials flex flex-col'
                        value={formData[field.key]}
                        onChange={(e) => handleChange(e, field.key)}
                    />
                </div>
            ))}
            <div className='flex justify-center'>
                <button type="submit" className='btn my-5 shadow-md' onClick={handleSubmit}>Zarejestruj</button>
            </div>
        </div>
    );
};

export default RegisterInput;
