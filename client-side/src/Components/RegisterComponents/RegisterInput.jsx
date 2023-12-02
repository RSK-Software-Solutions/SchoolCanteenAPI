import React, {useState} from 'react';
import axios from "axios";

const RegisterInput = () => {
    const [formData, setFormData] = useState({
        Firma: '',
        Email: '',
        Hasło: ''
    });

    const formFields = [
        {
            label: "Firma",
            key: "Firma"
        },
        {
            label: "Email",
            key: "Email"
        },
        {
            label: "Hasło",
            key: "Hasło"
        }
    ];

    const handleChange = (e, field) => {
        setFormData({
            ...formData,
            [field]: e.target.value
        });
    };

    const handleSubmit = () => {
        try {
            axios.post("http://localhost:5064/api/Company", formData)
                .then(function (response) {
                    console.log(response);
                }).catch(function (error) {
                console.log(error);
            });
        } catch (error) {
            console.error('Error:', error.message);
        }
    };


    return (
        <div>
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
                <button className='btn my-5 shadow-md' onClick={handleSubmit}>Zarejestruj</button>
            </div>
        </div>
    );
};

export default RegisterInput;
