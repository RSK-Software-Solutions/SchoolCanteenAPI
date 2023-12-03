import React, {useEffect, useState} from 'react';
import axios from 'axios';

const RegisterInput = () => {
    const [formData, setFormData] = useState({
        Name: '',
        Login: '',
        Password: ''
    });

    const URL = process.env.REACT_APP_URL;

    const formFields = [
        {label: 'Firma', key: 'Name'},
        {label: 'Email', key: 'Login'},
        {label: 'Hasło', key: 'Password'}
    ];

    const handleChange = (e, field) => {
        setFormData({
            ...formData,
            [field]: e.target.value
        });
    };

    useEffect(() => {
        getAllCompanies()
    }, []);
    const getAllCompanies = async () => {
        try {
            const companies = await axios.get(URL);
            console.log(formData.Name)
            console.log(companies.data.filter(s => s.name === formData.Name))
            return companies.data;
        } catch (err) {
            console.error(err)
        }
    }
    const HandleRegisterCompany = async (e) => {
        e.preventDefault();
        try {
            await axios.post(URL, formData);
        } catch (error) {
            console.error('Error:', error.message);
        }
    };

    return (
        <div>
            {formFields.map((field) => (
                <div key={field.key} className="mt-3 mx-3">
                    <label>{field.label}</label>
                    <input
                        type="text"
                        className="input-credentials flex flex-col"
                        value={formData[field.key]}
                        onChange={(e) => handleChange(e, field.key)}
                    />
                </div>
            ))}
            <div className="flex justify-center">
                <button type="button" className="btn my-5 shadow-md" onClick={HandleRegisterCompany}>
                    Zarejestruj
                </button>
            </div>
        </div>
    );
};

export default RegisterInput;
