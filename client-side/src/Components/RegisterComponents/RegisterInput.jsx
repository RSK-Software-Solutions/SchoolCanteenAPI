import React, {useState} from 'react';
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
        {label: 'Login', key: 'Login'},
        {label: 'Hasło', key: 'Password'}
    ];

    const handleChange = (e, field) => {
        setFormData({
            ...formData,
            [field]: e.target.value
        });
    };

    const isExistingCompany = async () => {
        try {
            const companies = await axios.get(URL);
            var exitingCompany = companies.data.filter(Firm => Firm.name === formData.Name)
            return exitingCompany.length > 0;

        } catch (err) {
            console.error(err)
        }
    }
    const HandleRegisterCompany = async (e) => {
        e.preventDefault();
        try {
            if (await isExistingCompany()) {
                return
            }

            if (!formData.Name) return
            if (!formData.Login) return
            if (!formData.Password) return

            await axios.post(URL, formData);
            setFormData({
                Name: "",
                Password: "",
                Login: "",
            })

        } catch (error) {
            return error;
        }
    };

    return (
        <div className='border'>
            {formFields.map((field) => (
                <div key={field.key}>
                    <label>{field.label}</label>
                    <input
                        type="text"
                        className='flex flex-col border'
                        value={formData[field.key]}
                        onChange={(e) => handleChange(e, field.key)}
                    />
                </div>
            ))}
            <div className='text-center'>
                <button type="button" onClick={HandleRegisterCompany}>
                    Zarejestruj
                </button>
            </div>
        </div>
    );
};

export default RegisterInput;
